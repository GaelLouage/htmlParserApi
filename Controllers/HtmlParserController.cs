using HtmlAgilityPack;
using HtmlParserApi.Dtos;
using HtmlParserApi.Entities;
using HtmlParserApi.Enums;
using HtmlParserApi.Helpers;
using HtmlParserApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using System.Drawing;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json.Nodes;
using System.Web;
using System.Xml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HtmlParserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HtmlParserController : ControllerBase
    {
        private readonly IHtmlAgilityService _htmlAgilityService;
        //cache the data on memory
        private readonly IMemoryCache _cache;
        // the key for the cached dictionary 
        private const string DataDictionaryCacheKey = "ScrapedDataDictionary";
        public HtmlParserController(IHtmlAgilityService htmlAgilityService, IMemoryCache cache)
        {
            _htmlAgilityService = htmlAgilityService;
            _cache = cache;
        }

        [HttpPost("/api/scrape/")]
        public async Task<IActionResult> PostHtmlToScrap(HtmlParserApi.Entities.HtmlEntity htmlEntity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var htmlperserDto = await _htmlAgilityService.GetHtmlFromUrlAsync(htmlEntity);
            if (htmlperserDto is null)
            {
                return NotFound("No Data found!");
            }
            if (htmlperserDto.parsedHtml is not null)
            {
                var scrapedDataDictionary = _cache
                    .GetOrCreate<Dictionary<int, byte[]>>(DataDictionaryCacheKey, entry => new Dictionary<int, byte[]>());

                scrapedDataDictionary.Add(scrapedDataDictionary.Count + 1, htmlperserDto.parsedHtml);
                // Update the dictionary in the cache
                _cache.Set(DataDictionaryCacheKey, scrapedDataDictionary);
            }
            return Ok(htmlperserDto);
        }

        [HttpGet("/api/scraped-data/{scrapeId}")]
        public IActionResult GetScrapedDataById(string scrapeId)
        {
            var scrapedDataDictionary = _cache.Get<Dictionary<int, byte[]>>(DataDictionaryCacheKey);
            return ScrapedDataHelper.ValidateScrapedById(scrapedDataDictionary, scrapeId);
        }

        [HttpGet("/api/scraped-data/{scrapeIds}/download/")]
        public IActionResult DownloadScrapedDataById(string scrapeIds, FileType filetype)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request!");
            }
            if (!int.TryParse(scrapeIds, out int id))
            {
                return BadRequest("Scrape id should be an integer!");
            }
            var scrapedDataDictionary = _cache.Get<Dictionary<int, byte[]>>(DataDictionaryCacheKey);
            if (scrapedDataDictionary is null)
            {
                return NotFound($"No scraped data with id {id} found!");
            }

            Dictionary<FileType, Func<FileStreamResult>> fileTypeDicionary = 
                new FileWriterHelper()
                .FileWriter(scrapedDataDictionary
                .FirstOrDefault(x => x.Key == id));
          
            return fileTypeDicionary[filetype]?.Invoke();
        }
    }
}