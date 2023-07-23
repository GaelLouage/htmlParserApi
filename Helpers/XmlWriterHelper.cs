using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace HtmlParserApi.Helpers
{
    public static class XmlWriterHelper
    {
        public static MemoryStream WriteToXmlFile(byte[] getData)
        {

            XmlDocument xmlDoc = new XmlDocument();

            XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDoc.AppendChild(xmlDeclaration);

            XmlElement rootElement = xmlDoc.CreateElement("ParsedHtmls");
            xmlDoc.AppendChild(rootElement);

            // Create book elements with attributes
            XmlElement dataParsedHtml = xmlDoc.CreateElement("ParsedHtml");
            dataParsedHtml.InnerText = System.Text.Encoding.UTF8.GetString(getData);
            rootElement.AppendChild(dataParsedHtml);

            var mem = new MemoryStream();
            xmlDoc.Save(mem);
            // Reset the memory stream position to the beginning before returning it
            mem.Seek(0, SeekOrigin.Begin);

            return mem;
        }
    }
}
