namespace WebCrawlerDataLayer.Data.DTOs;

public record URLToCrawlRequest
{
    public string Title { get; set; }
    
    //Logic is [element.class,class,class...etc]
    public IList<string> ElementsAndClassesToLook { get; set; }
    public string Url { get; set; }
};