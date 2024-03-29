using System.Text.RegularExpressions;
using HtmlAgilityPack;
using WebCrawlerAPI.Data;
using WebCrawlerAPI.Data.Models;

namespace WebCrawlerAPI.Services.CrawlerService;

class Crawler : ICrawler
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
    
    public async Task<List<Scrap>> StartCrawling()
    {
        List<Scrap> scraps = new List<Scrap>();
        var texturls = TextURLExtract();
        foreach (var url in texturls)
        {
            Scrap scrap = await Scrape(url);
            scraps.Add(scrap);
        }
        return scraps;
    }

    private async Task<Scrap> Scrape(string url)
    {
        Scrap scrap = new Scrap();
        HttpResponseMessage httpResponse = await _httpClient.GetAsync(url);
        string htmlContent = httpResponse.Content.ToString();
        HtmlDocument htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(htmlContent);
        //Get Wished Data


        //Get Other Links in Page
        scrap.ExtractedURLS = ExtractPageUrls(htmlDocument);
        return scrap;
    }

    private List<string> ExtractPageUrls(HtmlDocument htmlDocument)
    {
        List<string> extractedurls = new List<string>();
        var anchorNodes = htmlDocument.DocumentNode.SelectNodes("//a[@href");
        foreach (var anchornode in anchorNodes)
        {
            var link = anchornode.GetAttributeValue("href", "");
            extractedurls.Add(link);
        }
        return extractedurls;
    }

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