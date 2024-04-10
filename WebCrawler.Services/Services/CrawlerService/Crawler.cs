using System.Text.RegularExpressions;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Hosting;
using WebCrawlerDataLayer.Data;
using WebCrawlerDataLayer.Data.DTOs;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawler.Services.Services.CrawlerService;

public class Crawler : ICrawler
{
    private readonly HttpClient _httpClient;
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _hostenv;

    public Crawler(HttpClient httpClient, DataContext db, IWebHostEnvironment hostenv)
    {
        _httpClient = httpClient;
        _db = db;
        _hostenv = hostenv;
    }
    
    public async Task<ScrapsCollection> StartCrawling(CrawlRequest crawlRequest)
    {
        ScrapsCollection scrapsCollection = new ScrapsCollection();
        foreach (var urlToCrawlRequest in crawlRequest.UrlToCrawlRequests)
        {
            Scrap scrap = await Scrape(urlToCrawlRequest);
            scrapsCollection.Scraps.Add(scrap);
        }
        return scrapsCollection;
    }

    private async Task<Scrap> Scrape(URLToCrawlRequest urlToCrawlRequest)
    {
        Scrap scrap = new Scrap(){Url = urlToCrawlRequest.Url};
        HttpResponseMessage httpResponse = await _httpClient.GetAsync(scrap.Url);
        var htmlContent = await httpResponse.Content.ReadAsStringAsync();
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);
        
        //Get Wished Data
        scrap.Extracteddata = ExtractPageData(urlToCrawlRequest,htmlDocument);
        
        //Get Other Links in Page
        scrap.Extractedurls = ExtractPageUrls(htmlDocument);
        
        return scrap;
    }

    private void ExtractPageData(URLToCrawlRequest urlToCrawlRequest, HtmlDocument htmlDocument)
    {
        //select HTML elements and by classses
        foreach (var elementToSelect in urlToCrawlRequest.ElementsToLook)
        {
            var selectedelements = htmlDocument.DocumentNode.SelectNodes("");
        }
        //filter out by classes

    }
    
    private List<string> ExtractPageUrls(HtmlDocument htmlDocument)
    {
        List<string> extractedUrls = new List<string>();
        var anchorNodes = htmlDocument.DocumentNode.SelectNodes("a");
        foreach (var anchornode in anchorNodes)
        {
            var link = anchornode.GetAttributeValue("href", "");
            extractedUrls.Add(link);
        }
        return extractedUrls;
    }

    //METHOD TO WORK WITH GIVEN URLS IN TEXT FILES
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
    
}