using WebCrawlerDataLayer.Data.Models;

namespace WebCrawler.Services.Services.CrawlerService;

public interface ICrawler
{
    public Task<List<Scrap>> StartCrawling(string? crawlTitle);
}