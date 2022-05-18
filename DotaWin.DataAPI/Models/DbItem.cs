namespace DotaWin.Data.Models;

public class DbItem
{
    public enum Type
    {
        Boots = 0, Core, Neutral
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string TechnicalName { get; set; }
    public Type ItemType { get; set; }
    public int Price { get; set; }

    // Foreign Keys
    public ICollection<DbHeroItem> HeroItems { get; set; }

}
