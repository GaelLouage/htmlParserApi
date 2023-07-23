using Microsoft.AspNetCore.Mvc;

namespace HtmlParserApi.Helpers
{
    public static class ScrapedDataHelper
    {
        public static IActionResult  ValidateScrapedById(Dictionary<int, byte[]>? scrapedDataDictionary, string scrapeId)
        {
            if (scrapedDataDictionary is null)
            {
                return new NotFoundObjectResult("No scraped data found.");
            }
            if (!int.TryParse(scrapeId, out int id))
            {
                return  new BadRequestObjectResult("scrapedId has to be a valid integer.");
            }
            if (!scrapedDataDictionary.TryGetValue(id, out byte[] parsedHtml))
            {
                return  new NotFoundObjectResult($"Parsed data with ID: {id} not found!");
            }
            return new OkObjectResult(parsedHtml);
        }
    }

}
