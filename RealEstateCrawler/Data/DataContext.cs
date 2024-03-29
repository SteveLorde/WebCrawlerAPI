using Microsoft.EntityFrameworkCore;

namespace RealEstateCrawler.Data;

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