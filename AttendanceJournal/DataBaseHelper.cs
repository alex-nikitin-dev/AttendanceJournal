using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
namespace AttendanceJournal
{
    class DataBaseHelper
    {
        private static string Address = "specowka.ddns.net";
        private static string Port = "13110";
        private static string Id = "journaluser";
        private static string Pwd = "J1r5_jOngksnL_n8l11!";
        private static string DBName = "JournalDB";

        private static MySqlConnection GetNewConnection()
        {
            return new MySqlConnection($"Server={Address};Port={Port};database={DBName};User Id={Id};Password={Pwd};charset=utf8");
        }

        public static string GetUserPwdHash(string login)
        {
            string result = string.Empty;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT Password " +
                           "FROM JournalDB.User " +
                           $"WHERE login='{login}'",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    
                    for (int i = 0; reader.Read(); i++)
                    {
                        if(i > 0)
                        {
                            throw new Exception("database error: login is not unique");
                        }
                        result = (string)reader.GetValue(0);
                    }
                }


                con.Close();
            }

            return result;
        }

        public static bool IsServerAlive()
        {
            try
            {
                using (var con = GetNewConnection())
                {
                    con.Open();
                    con.Close();
                }
            }
            catch (MySqlException)
            {
                return false;
            }

            return true;
        }
    }
}