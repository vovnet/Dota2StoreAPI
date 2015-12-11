using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiProject
{
    public class TradeItem
    {
        public string ui_id { get; set; }
        public string i_name { get; set; }
        public string i_name_color { get; set; }
        public string i_rarity { get; set; }
        public string ui_status { get; set; }
        public string he_name { get; set; }
        public int ui_price { get; set; }
        public string i_classid { get; set; }
        public string i_instanceid { get; set; }
        public string i_quality { get; set; }
        public string i_market_hash_name { get; set; }
        public string i_market_name { get; set; }
        public float i_market_price { get; set; }
        public int position { get; set; }
        public int min_price { get; set; }
        public string ui_bid { get; set; }
        public string type { get; set; }
        public string ui_price_text { get; set; }
        public string i_market_price_text { get; set; }
    }

    public class AllTradeItems
    {
        public List<TradeItem> items { get; set; }
    }
}
