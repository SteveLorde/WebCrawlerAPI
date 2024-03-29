using RealEstateCrawler.Data;
using RealEstateCrawler.Services.CrawlerService;

namespace RealEstateCrawler.Services;

public static class ServicesRegister
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();
        serviceCollection.AddScoped<ICrawler,Crawler>();
    }
}