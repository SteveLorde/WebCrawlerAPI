using WebCrawlerDataLayer.Data.DTOs;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WebCrawler.Test;

public class CrawlerTest
{
    private HttpClient _httpClient = new HttpClient();
    private ITestOutputHelper _outputHelper = new TestOutputHelper();
    
    [Fact]
    public void CrawlerServiceTest()
    {
        URLCrawlRequest crawlRequest = new URLCrawlRequest()
        {

        };
        
        
    }
}