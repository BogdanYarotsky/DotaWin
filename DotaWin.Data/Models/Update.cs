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
        public DateOnly Date { get; set; }
        public bool Success { get; set; }
        public string Patch { get; set; } = string.Empty;

        // related entities
        public List<Hero> Heroes { get; set; } = new List<Hero>();
        public List<Item> Items { get; set; } = new List<Item>();
        public List<HeroItem> HeroItems { get; set; } = new List<HeroItem>();

    }
}
