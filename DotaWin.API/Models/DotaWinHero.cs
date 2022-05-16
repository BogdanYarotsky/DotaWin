namespace DotaWin.API.Models;

public class DotaWinHero
{
    public string Name { get; set; } = string.Empty;
    public double Winrate { get; set; }
    public ICollection<DotaWinItem> Items { get; set; } = new List<DotaWinItem>();
}
