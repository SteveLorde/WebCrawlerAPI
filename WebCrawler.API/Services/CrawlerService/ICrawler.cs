using WebCrawlerDataLayer.Data.Models;

namespace WebCrawlerAPI.Services.CrawlerService;

public interface ICrawler
{
    public Task<List<Scrap>> StartCrawling();
}