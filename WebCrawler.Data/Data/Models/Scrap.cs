namespace WebCrawlerDataLayer.Data.Models;

public class Scrap
{
    public Guid Id { get; set; } = new Guid();
    public string Title { get; set; }
    public string Url { get; set; }
    public IList<string> Extracteddata { get; set; }
    public IList<string> Extractedurls { get; set; }
}