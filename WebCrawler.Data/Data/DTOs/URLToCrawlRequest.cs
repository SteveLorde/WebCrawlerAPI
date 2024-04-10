namespace WebCrawlerDataLayer.Data.DTOs;

public record URLToCrawlRequest
{
    public string Title { get; set; }
    public IList<string> ElementsToLook { get; set; }
    public IList<string> ClassesToLook { get; set; }
    public IList<string> ClassesToAvoid { get; set; }
    public string Url { get; set; }
};