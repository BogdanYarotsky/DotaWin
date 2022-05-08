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
        public List<DotabuffItem> Items { get; set; }

        // MMR
        public double Winrate2K { get; set; }
        public double Winrate3K { get; set; }
        public double Winrate4K { get; set; }
        public double Winrate5K { get; set; }
        public double Winrate6K { get; set; }

        // Skill Bracket
        public double NormalWinrate { get; set; }
        public double HighWinrate { get; set; }
        public double VeryHighWinrate { get; set; }
    }
}
