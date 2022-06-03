using DotaWin.DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.DataLayer;

public class DotaWinDbContext : DbContext
{
    public DbSet<DbUpdate> DailyUpdates { get; set; }
    public DbSet<DbHero> Heroes { get; set; }
    public DbSet<DbItem> Items { get; set; }
    public DbSet<DbHeroItem> HeroItems { get; set; }

    public DotaWinDbContext(DbContextOptions<DotaWinDbContext> options) : base(options) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}
