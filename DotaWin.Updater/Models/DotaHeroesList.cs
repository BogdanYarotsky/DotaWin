using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Updater.Models
{
    public class DotaHeroesList
    {
        public DotaHeroesResult result { get; set; }
    }

    public class DotaHeroesResult
    {
        public Hero[] heroes { get; set; }
        public int status { get; set; }
        public int count { get; set; }
    }

    public class Hero
    {
        public string name { get; set; }
        public int id { get; set; }
        public string localized_name { get; set; }
    }
}
