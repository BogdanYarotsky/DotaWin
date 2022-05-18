using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Data.Models
{
    public class DbHero
    {
        // Data
        public int Id { get; set; }
        public string Name { get; set; }
        public string InternalName { get; set; }
        public IDictionary<string, double> Winrates { get; set; }

        // Relationships
        // public DbHeroBuild AbilityBuild { get; set; } add later
        public DbUpdate Update { get; set; }
        public ICollection<DbHeroItem> HeroItems { get; set; }
    }
}
