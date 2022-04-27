namespace DotaWin.Data.Models;

public class DbItem
{
    public enum Type
    {
        Boots = 0, Core, Neutral
    }

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ImgUrl { get; set; } = string.Empty;
    public Type ItemType { get; set; }
    public int Price { get; set; }

    // Foreign Keys
    public ICollection<DbUpdate> Updates { get; set; }
    public ICollection<DbHeroItem> HeroItems { get; set; }

}
