﻿using System;
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
        public static void AddNewStudent(string name, int group, int phone)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "INSERT INTO JournalDB.Student(NameOfStudent, GroupID, Phone, Head, UserID) " +
                           $"VALUES('{ name }', '{group}', '{phone}', '0', '3');", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public static int GetSubjectIDByName(string name)
        {
            int res = -1;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT ID " +
                           "FROM JournalDB.Subject " +
                           $"WHERE NameOfSubject='{name}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Subject is not unique");
                        }
                        res = (int)reader.GetValue(0);
                    }
                }
                con.Close();
            }
            return res;
        }
        public static string GetNameOfSubgectByID(int Id)
        {
            string res = null;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NameOfSubject " +
                           "FROM JournalDB.Subject " +
                           $"WHERE ID='{Id}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Subject is not unique");
                        }
                        res = (string)reader.GetValue(0);
                    }
                }
                con.Close();
            }
            return res;
        }
        public static List<Subject> GetListOfSubject()
        {
            List<Subject> res = new List<Subject>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NameOfSubject " +
                           "FROM JournalDB.Subject ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        res.Add(new Subject
                        {
                            nameofSubject = (string)reader.GetString(reader.GetOrdinal("NameOfSubject"))
                        });
                    }
                }
                con.Close();
            }
            return res;
        }
        public static void AddNewSubgect(string name)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "INSERT INTO JournalDB.Subject(NameOfSubject) " +
                           $"VALUES('{ name }');", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }

        }
        public static void AddNewGroup(int course, int groupNumber, int year)
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
                        listRes.Add(new Group
                        {
                            course = (int)reader.GetUInt32(reader.GetOrdinal("Course")),
                            group = (int)reader.GetInt32(reader.GetOrdinal("NumberOfGroup"))
                        });
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
                           "FROM JournalDB.Student " +
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
        public static int GetGroupIDByCorseAndNumber(int course, int groupNumber)
        {
            int result = -1;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT ID " +
                           "FROM JournalDB.GroupOfStudents " +
                           $"WHERE NumberOfGroup='{groupNumber}' AND Course='{course}';",
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
        public static int GetGroupIDByNumberAndYear(int group, int year)
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
        public static List<Professor> GetListOfProfessors()
        {
            List<Professor> res = new List<Professor>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NameOfProfessor, Phone, Room " +
                           "FROM JournalDB.Professor; ", con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        res.Add(new Professor
                        {
                            nameOfProfessor = reader.GetString(reader.GetOrdinal("NameOfProfessor")),
                            phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            room = reader.GetInt32(reader.GetOrdinal("Room"))
                        }); ;
                    }
                }
                con.Close();
            }

            return res;
        }
        public static void AddNewProfessor(string name, int phone, int room)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "INSERT INTO JournalDB.Professor(NameOfProfessor, Phone,Room) " +
                           $"VALUES('{ name }', '{phone}','{room}');", con);
                cmd.ExecuteNonQuery();
                con.Close();
            }
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
                        if (i > 0)
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