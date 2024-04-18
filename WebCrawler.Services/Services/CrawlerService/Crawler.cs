using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebCrawlerDataLayer.Data;
using WebCrawlerDataLayer.Data.DTOs;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawler.Services.Services.CrawlerService;

public class Crawler : ICrawler
{
    private readonly HttpClient _httpClient;
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _hostenv;
    private readonly IConfiguration _config;

    public Crawler(HttpClient httpClient, DataContext db, IConfiguration config ,IWebHostEnvironment hostenv)
    {
        _httpClient = httpClient;
        _db = db;
        _hostenv = hostenv;
        _config = config;
    }
    
    public async Task<string> StartCrawling(CrawlRequest crawlRequest)
    {
        ScrapsCollection scrapsCollection = new ScrapsCollection();
        foreach (var urlToCrawlRequest in crawlRequest.UrlToCrawlRequests)
        {
            Scrap scrap = await Scrape(urlToCrawlRequest);
            scrapsCollection.Scraps.Add(scrap);
        }
        //save data on file .CSV
        return await SaveDataOnFileAndReturn(scrapsCollection);
    }


    private async Task<Scrap> Scrape(URLCrawlRequest urlCrawlRequest)
    {
        var options = new ChromeOptions()
        {
            BinaryLocation = _config["ChromeBrowserBinary"]
        };            
        options.AddArguments("headless");
        var chrome = new ChromeDriver(options);
        chrome.Navigate().GoToUrl(urlCrawlRequest.Url);
        
        //Try Login
        Login(chrome, urlCrawlRequest);
        
        Scrap scrap = new Scrap(){Url = urlCrawlRequest.Url , Title = urlCrawlRequest.Title};
        var htmlContent = chrome.PageSource!;
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);
        
        //Get Wished Data
        scrap.Extracteddata = ExtractPageData(urlCrawlRequest,htmlDocument);
        
        //Get Other Links in Page
        scrap.Extractedurls = ExtractPageUrls(htmlDocument);
        
        return scrap;
    }

    private void Login(ChromeDriver browser, URLCrawlRequest urlCrawlRequest)
    {
        IWebElement usernameInput = browser.FindElement(By.Id("username"));
        IWebElement passwordInput = browser.FindElement(By.Id("password"));
        IWebElement submitButton = browser.FindElement(By.Id("submit"));
        // Enter credentials
        usernameInput.SendKeys($"{urlCrawlRequest.LoginCredential}");
        passwordInput.SendKeys($"{urlCrawlRequest.PasswordCredential}");
        // Submit the form
        submitButton.Click();
    }

    private List<string> ExtractPageData(URLCrawlRequest urlCrawlRequest, HtmlDocument htmlDocument)
    {
        //LOGIC DEPENDS HEAVILY ON KNOWING WEBPAGE'S HTML COMPLEXITY BEFOREHAND
        List<string> extractedData = new List<string>();
        //select HTML elements and by classses
        foreach (var elementToFind in urlCrawlRequest.ElementsAndClassesToLook)
        {
            var elementAndClasses = elementToFind.Split(".");
            var elementToSelect = elementAndClasses[0];
            var classesToSelect = elementAndClasses[1].Split(",");
            
            var selectedelements = htmlDocument.DocumentNode.SelectNodes($"{}");
        }
        return extractedData;
    }

    private async Task<string> SaveDataOnFileAndReturn(ScrapsCollection scrapsCollection)
    {
        StringBuilder savedData = new StringBuilder();
        DateTime crawlDate = DateTime.Now;
        savedData.AppendLine(crawlDate.ToString());
        foreach (var scrap in scrapsCollection.Scraps)
        {
            var titleanddate = $"{scrap.Title}" + "  " + $"{crawlDate}";
            savedData.AppendLine(titleanddate);
            savedData.AppendLine("Extracted Data");
            foreach (var data in scrap.Extracteddata)
            {
                savedData.AppendLine(data);
            }
            savedData.AppendLine("Extracted URLs");
            foreach (var extractedURL in scrap.Extractedurls)
            {
                savedData.AppendLine(extractedURL);
            }
            savedData.AppendLine("");
        }
        System.IO.File.WriteAllText($"Storage/{scrapsCollection.Id}/CrawlResult.csv", savedData.ToString());
        return savedData.ToString();
    }
    
    private List<string> ExtractPageUrls(HtmlDocument htmlDocument)
    {
        List<string> extractedUrls = new List<string>();
        var anchorNodes = htmlDocument.DocumentNode.SelectNodes("//a");
        foreach (var anchornode in anchorNodes)
        {
            var link = anchornode.GetAttributeValue("href", "");
            extractedUrls.Add(link);
        }
        return extractedUrls;
    }

    //METHOD TO WORK WITH GIVEN URLS IN TEXT FILES
    /*
    private List<string> TextURLExtract()
    {
        List<string> requestedUrls = new List<string>();
        string urlsFilePath = Path.Combine(_hostenv.ContentRootPath, "RealEsateLinks.txt");

        string fileContent = File.ReadAllText(urlsFilePath);
        string urlPattern = @"(http|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?";
        Regex regex = new Regex(urlPattern, RegexOptions.IgnoreCase);
        MatchCollection urlMatches = regex.Matches(fileContent);
        foreach (Match match in urlMatches)
        {
            requestedUrls.Add(match.Value);
        }
        return requestedUrls;
    }
    */
}