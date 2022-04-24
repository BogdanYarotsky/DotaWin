using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Data.Models
{
    public class Update
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Patch { get; set; } = string.Empty;

        // related entities

        // 130 heroes
        public List<Hero> Heroes { get; set; } = new List<Hero>();
        // 150 items
        public List<Item> Items { get; set; } = new List<Item>();
        // 4000 entries for today
        public List<HeroItem> HeroItems { get; set; } = new List<HeroItem>();

    }
}
