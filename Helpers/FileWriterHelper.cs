using HtmlParserApi.Enums;
using Microsoft.AspNetCore.Mvc;

namespace HtmlParserApi.Helpers
{
    public class FileWriterHelper : ControllerBase
    {
        public  Dictionary<FileType, Func<FileStreamResult>> FileWriter(KeyValuePair<int, byte[]> getData)
        {
            return new Dictionary<FileType, Func<FileStreamResult>>()
            {
                {FileType.json, () => File(WriteToJsonHelper.WriteToJson(getData), "application/octet-stream", "scrapedData.json")},
                {FileType.xml, () =>  File(XmlWriterHelper.WriteToXmlFile(getData.Value), "application/octet-stream", "scrapedData.xml") },
                {FileType.csv, () => File(WriteToCSVHelper.WriteToCSVFile(getData), "application/octet-stream", "scrapedData.csv")}
            };
        }
    }
}
