using Microsoft.AspNetCore.Mvc;
using WebCrawler.Services.Services.CrawlerService;
using WebCrawlerDataLayer.Data.DTOs;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawlerAPI.Controller;

[ApiController]
[Route("crawler")]
public class MainController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly ICrawler _crawlerService;

    public MainController(ICrawler crawlerService)
    {
        _crawlerService = crawlerService;
    }
    
    [HttpPost("startcrawler")]
    public async Task StartCrawl(CrawlRequest crawlRequest)
    {
        var savedData = await _crawlerService.StartCrawling(crawlRequest);
        
    }
    
    
}