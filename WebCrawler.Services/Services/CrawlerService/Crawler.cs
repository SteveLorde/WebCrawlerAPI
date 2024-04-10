using System.Text;
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
        //save data on file .CSV
        await SaveDataOnFileAndReturn(scrapsCollection);
        return scrapsCollection;
    }


    private async Task<Scrap> Scrape(URLToCrawlRequest urlToCrawlRequest)
    {
        Scrap scrap = new Scrap(){Url = urlToCrawlRequest.Url , Title = urlToCrawlRequest.Title};
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
        throw new NotImplementedException();
        
        //LOGIC DEPENDS HEAVILY ON KNOWING WEBPAGE'S HTML COMPLEXITY BEFOREHAND
        IList<string> extractedData = new List<string>();
        
        //select HTML elements and by classses
        foreach (var elementToFind in urlToCrawlRequest.ElementsAndClassesToLook)
        {
            var elementAndClasses = elementToFind.Split(".");
            var elementToSelect = elementAndClasses[0];
            var classesToSelect = elementAndClasses[1].Split(",");
            
            var selectedelements = htmlDocument.DocumentNode.SelectNodes("");
        }

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
        var anchorNodes = htmlDocument.DocumentNode.SelectNodes("a");
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