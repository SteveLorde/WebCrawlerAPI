using RealEstateCrawler.Data;

namespace RealEstateCrawler.Services.Crawler;

class Crawler : ICrawler
{
    private readonly HttpClient _httpClient;
    private readonly DataContext _db;

    public Crawler(HttpClient httpClient, DataContext db)
    {
        _httpClient = httpClient;
        _db = db;
    }
    
    public async Task StartCrawling()
    {
        throw new NotImplementedException();
    }

    private async Task Scrape()
    {
        
    }
}