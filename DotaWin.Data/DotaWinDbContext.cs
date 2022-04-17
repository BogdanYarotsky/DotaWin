using DotaWin.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace DotaWin.Data
{
    public class DotaWinDbContext : DbContext
    {
        public DbSet<Update> Updates { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<HeroItem> HeroItems { get; set; }

        // to change
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=my_pw");

    }
}