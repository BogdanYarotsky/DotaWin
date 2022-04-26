using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaWin.Updater.Models
{

    public class DotaItemsList
    {
        public DotaItemsResult result { get; set; }
    }

    public class DotaItemsResult
    {
        public Item[] items { get; set; }
        public int status { get; set; }
    }

    public class Item
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public int secret_shop { get; set; }
        public int side_shop { get; set; }
        public int recipe { get; set; }
        public string localized_name { get; set; }
    }
}
