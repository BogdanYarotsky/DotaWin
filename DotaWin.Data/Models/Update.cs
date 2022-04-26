using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Data.Models
{
    public class Update
    {
        // Data
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Patch { get; set; } = string.Empty;

        // Relationships
        public ICollection<Hero> Heroes { get; set; } // 130 heroes      
        public ICollection<Item> Items { get; set; } // 150 items
        public ICollection<HeroItem> HeroItems { get; set; } // 130*150 HeroItems

    }
}
