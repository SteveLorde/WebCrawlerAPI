using System.Text.RegularExpressions;
using RealEstateCrawler.Data;

namespace RealEstateCrawler.Services.Crawler;

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
    
    public async Task StartCrawling()
    {
        throw new NotImplementedException();
    }

    private async Task Scrape()
    {
        
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