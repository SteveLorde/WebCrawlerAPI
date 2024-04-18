namespace WebCrawlerDataLayer.Data.DTOs;

public record URLCrawlRequest
{
    public string Title { get; set; }
    
    //Logic is [element.class,class,class...etc]
    public List<string> ElementsAndClassesToLook { get; set; } = new List<string>();
    public string Url { get; set; } = "";
    public string LoginCredential { get; set; } = "";
    public string PasswordCredential { get; set; } = "";
};