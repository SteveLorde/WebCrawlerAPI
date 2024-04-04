using WebCrawler.Services.Services.CrawlerService;
using WebCrawlerDataLayer.Data;

namespace WebCrawlerAPI;

public static class ServicesRegister
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();
        serviceCollection.AddScoped<ICrawler,Crawler>();
    }
}