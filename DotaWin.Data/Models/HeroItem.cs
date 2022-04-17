using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Data.Models
{
    public class HeroItem
    {
        // Data
        public int Id { get; set; }
        public int Matches { get; set; }
        public double Winrate { get; set; }

        // Foreign Key
        public int HeroId { get; set; }
        public Hero Hero{ get; set; }

        // Foreign Key
        public int ItemId{ get; set; }
        public Item Item{ get; set; }

        // Foreign Key
        public int UpdateId { get; set; }
        public Update Update { get; set; }

    }
}
