using System.Text;
using WebCrawlerDataLayer.Data.DTOs;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace WebCrawler.Test;

public class CrawlerTest
{
    private HttpClient _httpClient = new HttpClient();
    private ITestOutputHelper _outputHelper = new TestOutputHelper();
    private string _crawlerServiceURL = "http://localhost:5010/crawler/startcrawler";
    
    [Fact]
    public async void CrawlerServiceTest()
    {
        URLCrawlRequest crawlRequest = new URLCrawlRequest()
        {
            Title = "Crawl Request Test",
            Url = " ",
            //Logic is [element.class,class,class...etc]
            ElementsAndClassesToLook = new List<string>() {"p.class1,class2", "div.class1,class2"},
            LoginCredential = "username",
            PasswordCredential = "password"
        };
        HttpContent requestContent = new StringContent(crawlRequest.ToString(), Encoding.UTF8, "application/json");
        HttpResponseMessage response = await _httpClient.PostAsync(_crawlerServiceURL, requestContent);
        
        //Return CSV FILE TO DOWNLOAD
        
    }
}