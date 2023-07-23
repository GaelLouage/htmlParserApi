using HtmlAgilityPack;
using HtmlParserApi.Dtos;

namespace HtmlParserApi.Services.Interfaces
{
    public interface IHtmlAgilityService
    {
        Task<HtmlParserDto> GetHtmlFromUrlAsync(HtmlParserApi.Entities.HtmlEntity htmlEntity);
    }
}