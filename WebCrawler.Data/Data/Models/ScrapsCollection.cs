namespace WebCrawlerDataLayer.Data.Models;

public class ScrapsCollection
{
    public Guid Id { get; set; } = new Guid();
    public IList<Scrap> Scraps { get; set; }
}