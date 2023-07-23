using HtmlAgilityPack;
using System.Web;

namespace HtmlParserApi.Extensions
{
    public static class NodeExtensions
    {
        public static async Task<HtmlNodeCollection?> GetNodes(this Entities.HtmlEntity htmlEntity, string xpathExpression)
        {
            var nodes = (await new HtmlWeb()
                .LoadFromWebAsync(
                 // decode the url so that %2f is replaced by /
                 HttpUtility.UrlDecode(htmlEntity.Url)))
                .DocumentNode
                .SelectNodes(xpathExpression);
            return nodes;
        }
    }
}
