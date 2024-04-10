namespace WebCrawlerDataLayer.Data.DTOs;

public record CrawlRequest
{
    public IList<URLToCrawlRequest> UrlToCrawlRequests { get; set; }
}