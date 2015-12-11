using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiProject
{
    public class ItemInfo
    {
        public string classid { get; set; }
        public string instanceid { get; set; }
        public string market_name { get; set; }
        public string name { get; set; }
        public string market_hash_name { get; set; }
        public string rarity { get; set; }
        public string quality { get; set; }
        public string type { get; set; }
        public string mtype { get; set; }
        public string slot { get; set; }
        public string min_price { get; set; }
        public string hash { get; set; }
    }

    public class MoneyInfo
    {
        public int money { get; set; }
    }

    public class Inventory
    {
        public string i_time { get; set; }
        public string i_status { get; set; }
        public bool success { get; set; }
    }

    public class Token
    {
        public string token { get; set; }
        public bool success { get; set; }
    }
}
