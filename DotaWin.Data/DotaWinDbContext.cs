using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Data
{
    public class DotaWinDbContext : DbContext
    {
        private readonly string _connectionString = "Host=localhost;Database=DotaWin;Username=postgres;Password=password1337";
        public DbSet<DbUpdate> DailyUpdates { get; set; }
        public DbSet<DbHero> Heroes { get; set; }
        public DbSet<DbItem> Items { get; set; }
        public DbSet<DbHeroItem> HeroItems { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

        // Helper functions
        public async Task<int> LastUpdateIdAsync() =>
            await DailyUpdates.AsNoTracking()
                              .OrderByDescending(upd => upd.Date)
                              .Select(upd => upd.Id)
                              .FirstOrDefaultAsync();
    }
}