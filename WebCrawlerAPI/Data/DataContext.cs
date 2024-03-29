using Microsoft.EntityFrameworkCore;

namespace WebCrawlerAPI.Data;

public class DataContext : DbContext
{
    public DataContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source=database.db");
    }
    
    
}