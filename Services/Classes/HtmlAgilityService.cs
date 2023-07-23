using HtmlAgilityPack;
using HtmlParserApi.Dtos;
using HtmlParserApi.Enums;
using HtmlParserApi.Extensions;
using HtmlParserApi.Services.Interfaces;
using System.Text;
using System.Web;
using System.Xml.XPath;

namespace HtmlParserApi.Services.Classes
{
    public class HtmlAgilityService : IHtmlAgilityService
    {

        public async Task<HtmlParserDto> GetHtmlFromUrlAsync(HtmlParserApi.Entities.HtmlEntity htmlEntity)
        {
            var htmlParserDto = new HtmlParserDto();

            var nodes =  await htmlEntity.GetNodes($"//{htmlEntity.TagName}");

            if (nodes is not null)
            {
                // get hthe base 64 string
                htmlParserDto.parsedHtml = Encoding.UTF8.GetBytes(string.Join("", nodes.Select(x => x.InnerHtml)));
                return htmlParserDto;
            }
            htmlParserDto.Errors.Add("Nodes are empty!");
            return htmlParserDto;
        }
    }
}
