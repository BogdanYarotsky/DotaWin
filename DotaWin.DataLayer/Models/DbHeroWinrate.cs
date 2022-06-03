
namespace DotaWin.DataLayer.Models;

public enum SkillLevel
{
    Normal, High, VeryHigh
}

public class DbHeroWinrate
{
    public int Id { get; set; }
    public SkillLevel Skill { get; set; }

    public double Value { get; set; }

    public int HeroId { get; set; }
    public DbHero Hero { get; set; }

}
