using HtmlAgilityPack;
using HtmlParserApi.Services.Classes;
using HtmlParserApi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HtmlParserApi.Extensions
{
    public static class CollectionExtensions
    {
        public static IServiceCollection HtmlAgilityServices(this WebApplicationBuilder builder)
        {
            return  builder.Services
                      .AddScoped<IHtmlAgilityService, HtmlAgilityService>()
                     .AddMemoryCache();
        }
    }
}
