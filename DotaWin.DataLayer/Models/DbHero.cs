namespace DotaWin.DataLayer.Models;

public class DbHero
{
    // Data
    public int Id { get; set; }
    public string Name { get; set; }
    public string InternalName { get; set; }
    public ICollection<DbHeroWinrate> Winrates { get; set; }

    // Relationships
    // public DbHeroBuild AbilityBuild { get; set; } add later
    public DbUpdate Update { get; set; }
    public ICollection<DbHeroItem> HeroItems { get; set; }
}
