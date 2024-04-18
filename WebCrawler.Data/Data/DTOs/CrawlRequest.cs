namespace WebCrawlerDataLayer.Data.DTOs;

public record CrawlRequest
{
    public List<URLCrawlRequest> UrlToCrawlRequests { get; set; } = new List<URLCrawlRequest>();
}