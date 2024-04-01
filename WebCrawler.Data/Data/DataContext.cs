using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebCrawlerDataLayer.Data.Models;

namespace WebCrawlerDataLayer.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration _config;

    public DataContext(IConfiguration config)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["DatabaseConnection"]);
    }
    
    public DbSet<Scrap> Scraps { get; set; }
    
    
}