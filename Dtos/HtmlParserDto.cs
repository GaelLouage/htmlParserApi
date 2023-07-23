namespace HtmlParserApi.Dtos
{
    public class HtmlParserDto
    {
        public List<string> Errors { get; set; } = new List<string>();
        public byte[]? parsedHtml { get;set; }
    }
}
