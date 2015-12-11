using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotaStoreAPI
{
    public class Dota2API
    {
        public static String SECRET_KEY = "L9LS83666qN1CHU688rGLuWH0aPiV8Q";
        public static String CSV_FILE_NAME = "itemsDota2.csv";


        public static DBData getDBName()
        {
            String responce = sendRequest("https://market.dota2.net/itemdb/current_570.json");
            DBData data = JsonConvert.DeserializeObject<DBData>(responce);
            return data;
        }

        public static void buy(StoreItem data)
        {
            string s = sendRequest("https://market.dota2.net/api/ItemInfo/" + data.clasSid + "_" + data.instanceId + "/ru/?key=" + SECRET_KEY);
            ItemInfo info = JsonConvert.DeserializeObject<ItemInfo>(s);
            string b = sendRequest("https://market.dota2.net/api/Buy/" + data.clasSid + "_" + data.instanceId + "/" + data.price + "/" + info.hash + "/?key=" + SECRET_KEY);
        }

        public static void loadDB(String dbName)
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://market.dota2.net/itemdb/" + dbName, CSV_FILE_NAME);
        }

        /// Парсит полученную таблицу и возвращает список DataStore
        public static List<StoreItem> parse()
        {
            List<StoreItem> result = new List<StoreItem>();
            using (StreamReader sr = new StreamReader(CSV_FILE_NAME))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    StoreItem p = new StoreItem();
                    p.parse(line);
                    result.Add(p);
                }
            }

            return result;
        }

        /**
         * Посылает запрос к апи магаза дотки
         * Принимает адрес http://
         * Возвращает строку с ответом в формате json
         * 
         * Служебный метод, самому его не нужно вызывать
         */
        private static string sendRequest(String path)
        {
            WebClient client = new WebClient();
            return client.DownloadString(path);
        }
    }
}
