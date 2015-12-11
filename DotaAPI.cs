using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiProject
{
    public class DotaAPI
    {
        /// <summary>
        /// Секретный ключ
        /// </summary>
        public static string SECRET_KEY = "L9LS83888qN1CHU688rGLuWH0aPiV8Q";

        /// <summary>
        /// Имя файла базы данных сохраняемого локально (например "itemsDota2.csv")
        /// </summary>
        public static string CSV_FILE_NAME = "itemsDota2.csv";


        /// <summary>
        /// Получает информацию о базе данных
        /// </summary>
        /// <returns>Возвращает объект DBInfo</returns>
        public static DBInfo getDBInfo()
        {
            string responce = sendRequest("https://market.dota2.net/itemdb/current_570.json");
            return JsonConvert.DeserializeObject<DBInfo>(responce);
        }


        public static int getMoney()
        {
            string responce = sendRequest("https://market.dota2.net/api/GetMoney/?key=" + SECRET_KEY);
            return JsonConvert.DeserializeObject<MoneyInfo>(responce).money;
        }


        /// <summary>
        /// Получает все предметы, которые есть у владельца
        /// </summary>
        /// <returns></returns>
        public static AllTradeItems trades()
        {
            string responce = sendRequest("https://market.dota2.net/api/Trades/?key=" + SECRET_KEY);
            responce = "{\"items\":" + responce + "}";

            return JsonConvert.DeserializeObject<AllTradeItems>(responce);
        }


        /// <summary>
        /// Изменить цену на предмет
        /// </summary>
        /// <param name="id">Ид предмета</param>
        /// <param name="price">Новая цена, если 0 - предмет будет снят с продажи</param>
        public static void setPrice(string id, int price)
        {
            string responce = sendRequest("https://market.dota2.net/api/SetPrice/" + id + "/" + price + "/?key=" + SECRET_KEY);
        }


        /// <summary>
        /// Установить новый токен для оффлайн обменов стим
        /// </summary>
        /// <param name="token"></param>
        public static void setToken(string token)
        {
            string responce = sendRequest("https://market.dota2.net/api/SetToken/" + token + "/?key=" + SECRET_KEY);
        }


        /// <summary>
        /// Устанавливает онлайн
        /// </summary>
        public static void setOnline()
        {
            string responce = sendRequest("https://market.dota2.net/api/PingPong/?key=" + SECRET_KEY);
        }


        /// <summary>
        /// Получает подробную информацию о предмете
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static ItemInfo itemInfo(StoreItem item)
        {
            string responce = sendRequest("https://market.dota2.net/api/ItemInfo/" + item.clasId + "_" + item.instanceId + "/ru/?key=1");
            return JsonConvert.DeserializeObject<ItemInfo>(responce);
        }

        /// <summary>
        /// Покупает предмет
        /// </summary>
        /// <param name="data"></param>
        public static void buy(StoreItem data)
        {
            string s = sendRequest("https://market.dota2.net/api/ItemInfo/" + data.clasId + "_" + data.instanceId + "/ru/?key=" + SECRET_KEY);
            ItemInfo info = JsonConvert.DeserializeObject<ItemInfo>(s);
            string b = sendRequest("https://market.dota2.net/api/Buy/" + data.clasId + "_" + data.instanceId + "/" + data.price + "/" + info.hash + "/?key=" + SECRET_KEY);
        }


        /// <summary>
        /// Запрашивает статус кэша инвентаря
        /// </summary>
        /// <returns></returns>
        public static Inventory inventoryStatus()
        {
            string responce = sendRequest("https://market.dota2.net/api/InventoryStatus/?key=" + SECRET_KEY);
            return JsonConvert.DeserializeObject<Inventory>(responce);
        }


        /// <summary>
        /// Отправляет запрос на обновление инвентаря
        /// </summary>
        /// <returns></returns>
        public static bool updateInventory()
        {
            string responce = sendRequest("https://market.dota2.net/api/UpdateInventory/?key=" + SECRET_KEY);
            return JsonConvert.DeserializeObject<Inventory>(responce).success;
        }


        /// <summary>
        /// Получает установленный токен
        /// </summary>
        /// <returns></returns>
        public static Token getToken()
        {
            string responce = sendRequest("https://market.dota2.net/api/GetToken/?key=" + SECRET_KEY);
            return JsonConvert.DeserializeObject<Token>(responce);
        }

        /// <summary>
        /// Отправка оффлайн трейда от нашего бота вам.
        /// </summary>
        /// <param name="inOut">Отправляем "out", если хотим вывести купленную вещь, "in" - если хотим передать боту продаваемую вещь.</param>
        /// <param name="botId"> При выводе купленной вещи, вы можете посмотреть этот id в свойствах предмета со страницы sell (api /Trades/). 1 - При передаче продаваемой вещи нашему боту, вместо [botid] передайте просто "1"</param>
        public static void itemRequest(string inOut, int botId)
        {
            string responce = sendRequest("https://market.dota2.net/api/ItemRequest/" + inOut + "/" + botId + "/?key=" + SECRET_KEY);
        }


        /// <summary>
        /// Загружает базу данных с сервера
        /// </summary>
        /// <param name="dbName">Имя базы данных</param>
        public static void loadDB(String dbName)
        {
            WebClient client = new WebClient();
            client.DownloadFile("https://market.dota2.net/itemdb/" + dbName, CSV_FILE_NAME);
        }

        /// <summary>
        /// Парсит локальную базу данных
        /// </summary>
        /// <returns>Список предметов в магазине</returns>
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
