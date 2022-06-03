using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.DataLayer.Models
{
    public class DbUpdate
    {
        // Data
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Patch { get; set; } = string.Empty;

        // Relationships
        public ICollection<DbHero> Heroes { get; set; } // 130 heroes      
        public ICollection<DbItem> Items { get; set; } // 150 items
        public ICollection<DbHeroItem> HeroItems { get; set; } // 130*150 HeroItems

    }
}
