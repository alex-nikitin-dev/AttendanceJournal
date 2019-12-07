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
        public static void AddNewStudent(string name, int group ,int year, int phone)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "INSERT INTO JournalDB.Student(NameOfStudent, GroupID, Phone, Head, UserID) " +
                           $"VALUES('{ name }', '{group * 10000 + year}', '{phone}', '0', '3');", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static void AddNewGroup( int course, int groupNumber, int year)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "INSERT INTO JournalDB.GroupOfStudents(Course, numberOfGroup, EntryYear) " +
                           $"VALUES('{ course }', '{groupNumber}', '{year}');", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        //
        public static List<Group> GetListOfGroup()
        {
            List<Group> listRes = new List<Group>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT Course, NumberOfGroup " +
                           "FROM JournalDB.GroupOfStudents ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        listRes.Add(new Group { 
                            course = (int)reader.GetUInt32(reader.GetOrdinal("Course")),
                            group = (int)reader.GetInt32(reader.GetOrdinal("NumberOfGroup"))}) ;   
                    }
                }
                con.Close();
            }
            return listRes;
        }
        public static List<Students> GetListOfStudentsByGroupID(int groupID)
        {
            List<Students> listRes = new List<Students>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NameOfStudent, GroupID, Phone, Head " +
                           "FROM JournalDB.Student "+
                           $"WHERE GroupID='{groupID}'",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        listRes.Add(new Students
                        {
                            Name = reader.GetString(reader.GetOrdinal("NameOfStudent")),
                            Group = reader.GetInt32(reader.GetOrdinal("GroupID")),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            Head = reader.GetBoolean(reader.GetOrdinal("Head"))

                        });
                    }
                }
                con.Close();
            }

            return listRes;
        }
        public static int GetGroupIDByCorseAndNumber(int corse, int groupNumber)
        {
            int result = -1;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT ID " +
                           "FROM JournalDB.GroupOfStudents " +
                           $"WHERE NumberOfGroup='{groupNumber}' AND Corse='{corse}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: group is not unique");
                        }
                        result = (int)reader.GetValue(0);
                    }
                }
                con.Close();
            }

            return result;

        }
        public static int GetGroupIDByNumberAndYear(int group, int year )
        {
            int result = -1;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT ID " +
                           "FROM JournalDB.GroupOfStudents " +
                           $"WHERE NumberOfGroup='{group}' AND Entryyear='{year}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: group is not unique");
                        }
                        result = (int)reader.GetValue(0);
                    }
                }
                con.Close();
            }

            return result;

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