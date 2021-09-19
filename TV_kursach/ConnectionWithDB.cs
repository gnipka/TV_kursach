using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_kursach
{
    public static class ConnectionWithDB
    {
        readonly static MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=tv");
        public static void OpenConnection()
        {
            if(connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public static void CloseConnection()
        {
            if(connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public static MySqlConnection GetConnection()
        {
            return connection;
        }

    }
}
