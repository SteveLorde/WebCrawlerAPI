using RealEstateCrawler.Data.Models;

namespace RealEstateCrawler.Services.Crawler;

public interface ICrawler
{
    public Task<List<Scrap>> StartCrawling();
}