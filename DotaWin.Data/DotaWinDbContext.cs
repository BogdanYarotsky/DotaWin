using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Data;

public class DotaWinDbContext : DbContext
{
    public DbSet<DbUpdate> DailyUpdates { get; set; }
    public DbSet<DbHero> Heroes { get; set; }
    public DbSet<DbItem> Items { get; set; }
    public DbSet<DbHeroItem> HeroItems { get; set; }
    //public DotaWinDbContext(DbContextOptions options) : base(options) { }
    public DotaWinDbContext() 
    {
        Database.SetConnectionString("Host=localhost;Database=DotaWin;Username=postgres;Password=password1337");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        optionsBuilder.UseNpgsql();
    }
}
