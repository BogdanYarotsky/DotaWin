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
        public string ImgUrl { get; set; }
        public double Winrate { get; set; }
        // Relationships
        public DbUpdate Update { get; set; }
        public ICollection<DbHeroItem> HeroItems { get; set; }
    }
}
