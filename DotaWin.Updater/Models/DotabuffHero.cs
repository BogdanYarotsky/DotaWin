using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Updater.Models
{
    public class DotabuffHero
    {
        public string HeroName { get; set; }
        public double Winrate { get; set; }
        public int TotalMatches { get; set; }
        public List<DotabuffItem> Items { get; set; }
    }
}
