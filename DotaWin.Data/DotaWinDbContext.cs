using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DotaWin.Data
{
    public class DotaWinDbContext : DbContext
    {
        private readonly string _connectionString = "Host=localhost;Database=DotaWin;Username=postgres;Password=password1337";
        public DbSet<DbUpdate> DailyUpdates { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(_connectionString);

        // Helper functions
        public async Task<DbUpdate> MostRecentUpdateAsync() =>
            await DailyUpdates.OrderByDescending(upd => upd.Date).FirstOrDefaultAsync();
    }
}