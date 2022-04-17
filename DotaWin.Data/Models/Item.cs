using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Data.Models
{
    public class Item
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
        public int UpdateId { get; set; }
        public Update Update { get; set; }
        public List<HeroItem> HeroItems { get; set; } = new List<HeroItem>();

    }
}
