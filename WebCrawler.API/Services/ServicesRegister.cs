﻿using WebCrawlerAPI.Services.CrawlerService;
using WebCrawlerDataLayer.Data;

namespace WebCrawlerAPI.Services;

public static class ServicesRegister
{
    public static void AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<DataContext>();
        serviceCollection.AddScoped<ICrawler,Crawler>();
    }
}