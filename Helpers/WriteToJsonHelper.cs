using HtmlParserApi.Entities;
using Newtonsoft.Json;
using System.Text;

namespace HtmlParserApi.Helpers
{
    public static class WriteToJsonHelper
    {
        public static MemoryStream WriteToJson(KeyValuePair<int, byte[]> getData)
        {
            var serializedDictionary = JsonConvert.SerializeObject
                     (
                     new ParsedDataEntity(getData.Key, System.Text.Encoding.UTF8.GetString(getData.Value)
                     ));

           return new MemoryStream(Encoding.UTF8.GetBytes(serializedDictionary));
        }
    }
}
