namespace DotaWin.Data.Models;

public class DbHeroBuild
{
    public enum TalentTreeLeaf
    {
        Left = 0, Right
    }
    public ICollection<TalentTreeLeaf> Talents { get; set; }
}