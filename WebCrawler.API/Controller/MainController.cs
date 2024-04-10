using Microsoft.AspNetCore.Mvc;
using WebCrawler.Services.Services.CrawlerService;
using WebCrawlerDataLayer.Data.DTOs;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawlerAPI.Controller;

[ApiController]
[Route("crawler")]
public class MainController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ICrawler _crawler;

    public MainController(ICrawler crawler)
    {
        _crawler = crawler;
    }
    
    [HttpPost("startcrawler")]
    public async Task<List<Scrap>> StartCrawl(CrawlRequest crawlRequest)
    {
        return await _crawler.StartCrawling(crawlRequest);
    }
    
    
}