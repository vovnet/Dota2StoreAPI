using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotaApiProject
{
    public class DBInfo
    {
        /// <summary>
        /// Временная метка
        /// </summary>
        public int time { get; set; }

        /// <summary>
        /// Имя базы данных (*.csv)
        /// </summary>
        public String db { get; set; }
    }
}
