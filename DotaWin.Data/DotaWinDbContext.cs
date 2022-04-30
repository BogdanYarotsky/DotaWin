using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Data;

public class DotaWinDbContext : DbContext
{
    public DbSet<DbUpdate> DailyUpdates { get; set; }
    public DbSet<DbHero> Heroes { get; set; }
    public DbSet<DbItem> Items { get; set; }
    public DbSet<DbHeroItem> HeroItems { get; set; }
    public DotaWinDbContext(DbContextOptions options) : base(options) { }
}
