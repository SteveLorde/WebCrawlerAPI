using RealEstateCrawler.Data.Models;

namespace RealEstateCrawler.Services.CrawlerService;

public interface ICrawler
{
    public Task<List<Scrap>> StartCrawling();
}