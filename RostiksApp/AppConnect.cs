using RostiksApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RostiksApp
{
    class AppConnect
    {
        // Контекст базы данных 
        public static RostiksDBEntities DBContext;

        // Текущий авторизованный сотрудник (null если гость)
        public static Employees CurrentUser;

        // Метод для подключения к БД 
        public static void ConnectToDatabase()
        {
            DBContext = new RostiksDBEntities();
        }
    }
}
