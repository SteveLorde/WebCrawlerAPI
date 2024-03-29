using Microsoft.AspNetCore.Mvc;
using WebCrawlerAPI.Data.Models;
using WebCrawlerAPI.Services.CrawlerService;

namespace WebCrawlerAPI.Controller;

[ApiController]
[Route("crawler")]
public class APIController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ICrawler _crawler;

    public APIController(ICrawler crawler)
    {
        _crawler = crawler;
    }
    
    [HttpGet("startcrawler")]
    public async Task<List<Scrap>> StartCrawl()
    {
        return await _crawler.StartCrawling();
    }
    
    
}