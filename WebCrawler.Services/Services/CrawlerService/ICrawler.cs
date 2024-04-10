using WebCrawlerDataLayer.Data.DTOs;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawler.Services.Services.CrawlerService;

public interface ICrawler
{
    public Task<ScrapsCollection> StartCrawling(CrawlRequest crawlRequest);
}