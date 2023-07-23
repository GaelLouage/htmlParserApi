using HtmlParserApi.Dtos;

namespace HtmlParserApi.Extensions
{
    public static class ScrapedDataExtensions
    {
        public static void AddScrapedDataToDictionary(this  Dictionary<int, byte[]> dataDictinary,HtmlParserDto htmlParserDto)
        {
            if(htmlParserDto.parsedHtml is not null)
            {
                dataDictinary.Add(dataDictinary.Count + 1, htmlParserDto.parsedHtml);
            }
        }
    }
}
