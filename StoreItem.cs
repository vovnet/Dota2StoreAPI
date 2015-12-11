using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotaApiProject
{
    public class StoreItem
    {
        public string clasId { get; set; }
        public string instanceId { get; set; }
        public string price { get; set; }
        public string offerst { get; set; }
        public string popularity { get; set; }
        public string rarity { get; set; }
        public string quality { get; set; }
        public string heroId { get; set; }
        public string marketName { get; set; }
        public string nameColor { get; set; }
        public string pop { get; set; }

        public void parse(string line)
        {
            string[] parts = line.Split(';');  //Разделитель в CSV файле.
            if (parts.Length < 9) return;
            clasId = parts[0];
            instanceId = parts[1];
            price = parts[2];
            offerst = parts[3];
            popularity = parts[4];
            rarity = parts[5];
            quality = parts[6];
            heroId = parts[7];
            marketName = parts[8];
        }
    }
}
