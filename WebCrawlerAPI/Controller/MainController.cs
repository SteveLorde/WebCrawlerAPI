using Microsoft.AspNetCore.Mvc;
using WebCrawlerAPI.Services.CrawlerService;
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
    
    [HttpGet("startcrawler")]
    public async Task<List<Scrap>> StartCrawl()
    {
        return await _crawler.StartCrawling();
    }
    
    
}