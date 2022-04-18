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

        // sets connection string
        // https://www.npgsql.org/efcore/
        // https://www.npgsql.org/doc/connection-string-parameters.html
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=DotaWin;Username=postgres;Password=password1337");
    }
}