using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.DataLayer.Models
{
    public class DbHeroItem
    {
        // Data
        public int Id { get; set; }
        public int Matches { get; set; }
        public double Winrate { get; set; }

        // Foreign Key
        public int HeroId { get; set; }
        public DbHero Hero { get; set; }

        // Foreign Key
        public int ItemId { get; set; }
        public DbItem Item { get; set; }

        // Foreign Key
        public int UpdateId { get; set; }
        public DbUpdate Update { get; set; }

    }
}
