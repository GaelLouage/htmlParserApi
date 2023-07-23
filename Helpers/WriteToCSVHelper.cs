using System.Text;

namespace HtmlParserApi.Helpers
{
    public static class WriteToCSVHelper
    {
        public static MemoryStream WriteToCSVFile(KeyValuePair<int, byte[]> getData)
        {
            int key = getData.Key;
            string csvData = System.Text.Encoding.UTF8.GetString(getData.Value);
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("ID,Data"); 
            sb.AppendLine($"{key},{csvData}"); 

            byte[] csvBytes = Encoding.UTF8.GetBytes(sb.ToString());

            return new MemoryStream(csvBytes);
        }
    }
}
