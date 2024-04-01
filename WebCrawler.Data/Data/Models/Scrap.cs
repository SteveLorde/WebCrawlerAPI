namespace WebCrawlerDataLayer.Data.Models;

public class Scrap
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> Extracteddata { get; set; }
    public List<string> Extractedurls { get; set; }
}