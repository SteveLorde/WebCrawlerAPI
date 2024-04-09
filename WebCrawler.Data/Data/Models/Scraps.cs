namespace WebCrawlerDataLayer.Data.Models;

public class Scraps
{
    public Guid Id { get; set; } = new Guid();
    public string Title { get; set; }
    public IList<Scrap> ScrapsList { get; set; }
}