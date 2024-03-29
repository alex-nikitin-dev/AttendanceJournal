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
        private static MySqlConnection GetNewConnection()
        {
            return new MySqlConnection($"Server={ServerAuthorization.ServerAddress};Port={ServerAuthorization.ServerPort}"+
                $";database={ServerAuthorization.DataBaseName};User Id={ServerAuthorization.UserId};Password={ServerAuthorization.DataBasePwd};charset=utf8");
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
        public static Subject GetSubgectByID(int id)
        {
            Subject res = null;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.Subject " +
                           $"WHERE ID='{id}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Subject is not unique");
                        }
                        res = new Subject
                        {
                            ID = id,
                            nameofSubject = reader.GetString(reader.GetOrdinal("NameOfSubject"))
                        };
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
                           "SELECT * " +
                           "FROM JournalDB.Subject ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        res.Add(new Subject
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            nameofSubject = reader.GetString(reader.GetOrdinal("NameOfSubject"))
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
                           "SELECT * " +
                           "FROM JournalDB.GroupOfStudents ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        listRes.Add(new Group
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            course = (int)reader.GetUInt32(reader.GetOrdinal("Course")),
                            entryYear = (int)reader.GetUInt32(reader.GetOrdinal("EntryYear")),
                            group = (int)reader.GetInt32(reader.GetOrdinal("numberOfGroup"))
                        });
                    }
                }
                con.Close();
            }
            return listRes;
        }

        public static Group GetGroupByID(int id)
        {
            Group res = null;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.GroupOfStudents " +
                           $"WHERE ID='{id}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Subject is not unique");
                        }
                        res = new Group
                        {
                            ID = id,
                            course = (int)reader.GetUInt32(reader.GetOrdinal("Course")),
                            group = (int)reader.GetInt32(reader.GetOrdinal("NumberOfGroup"))
                        };
                    }
                }
                con.Close();
            }
            return res;
        }
        public static List<Students> GetListOfStudentsByGroupID(int groupID)
        {
            List<Students> listRes = new List<Students>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.Student " +
                           $"WHERE GroupID='{groupID}'",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        listRes.Add(new Students
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Name = reader.GetString(reader.GetOrdinal("NameOfStudent")),
                            Group = GetGroupByID(reader.GetInt32(reader.GetOrdinal("GroupID"))),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            Head = reader.GetBoolean(reader.GetOrdinal("Head"))

                        }); ;
                    }
                }
                con.Close();
            }

            return listRes;
        }

        public static List<Students> GetListOfStudents()
        {
            List<Students> listRes = new List<Students>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.Student ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {

                    for (int i = 0; reader.Read(); i++)
                    {
                        listRes.Add(new Students
                        {
                            ID = reader.GetInt32(reader.GetOrdinal("ID")),
                            Name = reader.GetString(reader.GetOrdinal("NameOfStudent")),
                            Group = GetGroupByID(reader.GetInt32(reader.GetOrdinal("GroupID"))),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            Head = reader.GetBoolean(reader.GetOrdinal("Head"))

                        });
                    }
                }
                con.Close();
            }

            return listRes;
        }
        public static Students GetStudentByID(int id)
        {
            Students res = null;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.Student " +
                           $"WHERE ID='{id}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Student is not unique");
                        }
                        res = new Students
                        {
                            ID = id,
                            Name = reader.GetString(reader.GetOrdinal("NameOfStudent")),
                            Group = GetGroupByID(reader.GetInt32(reader.GetOrdinal("GroupID"))),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            Head = reader.GetBoolean(reader.GetOrdinal("Head"))
                        };
                    }
                }
                con.Close();
            }
            return res;
        }
            public static List<Mark> GetListOfCountMarksByStudentID(List<Students> students)
            {
                List<Mark> res = new List<Mark>();
                for (int i = 0; i < students.Count; i++)
                {
                    using (var con = GetNewConnection())
                    {
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(
                                   "SELECT COUNT(Mark)" +
                                   "FROM JournalDB.Journal " +
                                   $"WHERE Mark='0' AND StudentID='{students[i].ID}'",
                                   con);
                        using (var reader = cmd.ExecuteReader())
                        {
                            reader.Read();
                            res.Add(new Mark
                            {
                                mark = reader.GetInt32(0)

                            });
                        }
                        con.Close();
                    }
                }
                return res;
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

            public static Professor GetProfessorByID(int id)
            {
                Professor res = null;
                using (var con = GetNewConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                               "SELECT * " +
                               "FROM JournalDB.Professor " +
                               $"WHERE ID='{id}';",
                               con);
                    using (var reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; reader.Read(); i++)
                        {
                            if (i > 0)
                            {
                                throw new Exception("database error: Subject is not unique");
                            }
                            res = new Professor
                            {
                                ID = id,
                                nameOfProfessor = reader.GetString(reader.GetOrdinal("NameOfProfessor")),
                                phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                                room = reader.GetInt32(reader.GetOrdinal("Room"))
                            };
                        }
                    }
                    con.Close();
                }
                return res;
            }
      
            public static List<Professor> GetListOfProfessors()
            {
                List<Professor> res = new List<Professor>();
                using (var con = GetNewConnection())
                {
                    con.Open();
                    MySqlCommand cmd = new MySqlCommand(
                               "SELECT * " +
                               "FROM JournalDB.Professor; ", con);
                    using (var reader = cmd.ExecuteReader())
                    {

                        for (int i = 0; reader.Read(); i++)
                        {
                            res.Add(new Professor
                            {
                                ID = reader.GetInt32(reader.GetOrdinal("ID")),
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
       
        public static int GetMarksByIDAndDate(int id, DateTime dateFrom, DateTime dateTo)
        {
            int res = 0;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT Count(Mark) as cnt " +
                           "FROM JournalDB.Journal " +
                           "WHERE StudentID = " + id + " " +
                           "AND Mark = '1' " +
                           "AND EntryDate BETWEEN '" + dateFrom.ToString("dd.MM.yyyy") + "' AND '" + dateTo.ToString("dd.MM.yyyy") + "' ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        res = reader.GetInt32(reader.GetOrdinal("cnt"));
                    }
                }
                con.Close();
            }
            return res;
        }
      
        public static List<Entry> GetListOfDayEntriesByGroupIDAndDate(int id, DateTime date)
        {
            List<Entry> res = new List<Entry>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT StudentID, COUNT(Mark) as cnt, EntryDate " +
                           "FROM JournalDB.Journal INNER JOIN JournalDB.Student " +
                           "ON JournalDB.Journal.StudentID = JournalDB.Student.ID " +
                           "AND JournalDB.Student.GroupID = " + id + " " +
                           "AND Mark = '" + 1 + "' " +
                           "AND JournalDB.Journal.EntryDate = STR_TO_DATE('" + date.ToString("dd.MM.yyyy") + "', '%d.%m.%Y') " +
                           "GROUP BY StudentID, EntryDate ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        //System.Diagnostics.Debug.WriteLine(reader.GetDateTime(reader.GetOrdinal("EntryDate")).Date.ToString("dd.MM.yyyy"));
                        res.Add(new Entry
                        {
                            Student = GetStudentByID(reader.GetInt32(reader.GetOrdinal("StudentID"))),
                            Mark = reader.GetInt32(reader.GetOrdinal("cnt"))
                        }); ;
                    }
                }
                con.Close();
            }
            return res;
        }
      
        public static List<Entry> GetListOfDayDelailEntriesByGroupIDAndDate(int id, DateTime date)
        {
            List<Entry> res = new List<Entry>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NumberOfLesson, Room, SubjectId, ProfessorId, EntryDate " +
                           "FROM JournalDB.Journal INNER JOIN JournalDB.Student " +
                           "ON JournalDB.Journal.StudentID = JournalDB.Student.ID " +
                           "AND JournalDB.Student.GroupID = " + id + " " +
                           "AND JournalDB.Journal.EntryDate = STR_TO_DATE('" + date.ToString("dd.MM.yyyy") + "', '%d.%m.%Y') " +
                           "GROUP BY NumberOfLesson, Room, SubjectId, ProfessorId, EntryDate ",
                           con);
                    using (var reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; reader.Read(); i++)
                        {
                                res.Add(new Entry
                            {     
                            NumberOfLesson = reader.GetInt32(reader.GetOrdinal("NumberOfLesson")),
                            Room = reader.GetInt32(reader.GetOrdinal("Room")),
                            Professor = GetProfessorByID(reader.GetInt32(reader.GetOrdinal("ProfessorID"))),
                            Subject = GetSubgectByID(reader.GetInt32(reader.GetOrdinal("SubjectID")))
                            }); ;
                        }
                    }
                con.Close();
               }
            return res;
        }
      
        public static List<Entry> GetListOfLessonDelailEntriesByLessonNumbAndDate(int id, int numberOfLesson, DateTime date)
        {
            List<Entry> res = new List<Entry>();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT StudentID, Mark " +
                           "FROM JournalDB.Journal INNER JOIN JournalDB.Student " +
                           "ON JournalDB.Journal.StudentID = JournalDB.Student.ID " +
                           "AND JournalDB.Student.GroupID = " + id + " " +
                           "AND JournalDB.Journal.NumberOfLesson = " + numberOfLesson + " " +
                           "AND JournalDB.Journal.EntryDate = STR_TO_DATE('" + date.ToString("dd.MM.yyyy") + "', '%d.%m.%Y') ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                            res.Add(new Entry
                            {
                                EntryDate = reader.GetDateTime(reader.GetOrdinal("EntryDate")),
                                NumberOfLesson = reader.GetInt32(reader.GetOrdinal("NumberOfLesson")),
                                Room = reader.GetInt32(reader.GetOrdinal("Room")),
                                Professor = GetProfessorByID(reader.GetInt32(reader.GetOrdinal("ProfessorID"))),
                                Subject = GetSubgectByID(reader.GetInt32(reader.GetOrdinal("SubjectID"))),
                                Student = GetStudentByID(reader.GetInt32(reader.GetOrdinal("StudentID"))),
                                Mark = int.Parse(reader.GetBoolean(reader.GetOrdinal("Mark")).ToString())
                            }); ;
                     }
                 }
                    con.Close();
             }
            return res;
        }
      
        public static Entry GetEntryDetailsByLessonNumbAndDate(int id, int numberOfLesson, DateTime date)
        {
            Entry res = new Entry();
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT ProfessorID, SubjectID, Room " +
                           "FROM JournalDB.Journal INNER JOIN JournalDB.Student " +
                           "ON JournalDB.Journal.StudentID = JournalDB.Student.ID " +
                           "AND JournalDB.Student.GroupID = " + id + " " +
                           "AND JournalDB.Journal.NumberOfLesson = " + numberOfLesson + " " +
                           "AND JournalDB.Journal.EntryDate = STR_TO_DATE('" + date.ToString("dd.MM.yyyy") + "', '%d.%m.%Y') ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        res = new Entry
                        {
                            NumberOfLesson = numberOfLesson,
                            EntryDate = date,
                            Room = reader.GetInt32(reader.GetOrdinal("Room")),
                            Professor = GetProfessorByID(reader.GetInt32(reader.GetOrdinal("ProfessorID"))),
                            Subject = GetSubgectByID(reader.GetInt32(reader.GetOrdinal("SubjectID")))
                        };
                    }
                }
                con.Close();
            }
            return res;
        }
      
        public static bool ContainsEntry(int id, int numberOfLesson, DateTime date)
        {
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT NumberOfLesson, Room, SubjectId, ProfessorId, EntryDate " +
                           "FROM JournalDB.Journal INNER JOIN JournalDB.Student " +
                           "ON JournalDB.Journal.StudentID = JournalDB.Student.ID " +
                           "AND JournalDB.Student.GroupID = " + id + " " +
                           "AND JournalDB.Journal.NumberOfLesson = " + numberOfLesson + " " +
                           "AND JournalDB.Journal.EntryDate = STR_TO_DATE('" + date.ToString("dd.MM.yyyy") + "', '%d.%m.%Y') ",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i == 0)
                        {
                            return true;
                        }
                    }
                }
                con.Close();
            }
            return false;
        }
      
        public static int GetSemesterByGroupIDAndDate(int id, DateTime date)
        {
            Group group = GetGroupByID(id);
            if (date.Year > group.entryYear + group.course || date.Year < group.entryYear)
                return -1;
            if (group.entryYear + group.course == date.Year && date.Month > 8)
                return -1;
            if (group.entryYear + group.course - 1 == date.Year)
                return 1;
            else
                return 2;
        }
      
        public static int GetWeekNumberByGroupIDAndDate(int id, DateTime date)
        {
            Group group = GetGroupByID(id);
            int semester = GetSemesterByGroupIDAndDate(id, date);
            if(semester == 1)
            {
                DateTime start = new DateTime(date.Year, 9, 1);
                if (date >= start && date <= new DateTime(date.Year + 1, 1, 1))
                {
                    return date.Subtract(start).Days / 7 + 1;
                }
            }
            if (semester == 2)
            {
                DateTime start = new DateTime(date.Year, 1, 1);
                if (date >= start && date <= new DateTime(date.Year, 7, 1))
                {
                    return date.Subtract(start).Days / 7 + 1;
                }
            }
            return -1;
        }

        public static Students GetStudentByUserID(int id)
        {
            Students res = null;
            using (var con = GetNewConnection())
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand(
                           "SELECT * " +
                           "FROM JournalDB.Student " +
                           $"WHERE UserID='{id}';",
                           con);
                using (var reader = cmd.ExecuteReader())
                {
                    for (int i = 0; reader.Read(); i++)
                    {
                        if (i > 0)
                        {
                            throw new Exception("database error: Subject is not unique");
                        }
                        res = new Students
                        {
                            ID = id,
                            Name = reader.GetString(reader.GetOrdinal("NameOfStudent")),
                            Group = GetGroupByID(reader.GetInt32(reader.GetOrdinal("GroupID"))),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")),
                            Head = reader.GetBoolean(reader.GetOrdinal("Head"))
                        };
                    }
                }
                con.Close();
            }
            return res;
        }
      
            public static void AddNewEntry(Entry entry)
            {
                using (var con = GetNewConnection())
                {
                    con.Open();
#warning a;lsdkjfl;asdkjf;llaskdjf;lskdjf;laskfj
                MySqlCommand cmd = new MySqlCommand(
                               "INSERT INTO JournalDB.Journal(EntryDate, NumberOfLesson, Room, ProfessorID, SubjectID, StudentID, Mark) " +
                               $"VALUES('{ entry.EntryDate.Date.ToString("yyyy-MM-dd") }', '{entry.NumberOfLesson}', '{entry.Room}', '{entry.Professor.ID}', '{entry.Subject.ID}', '{entry.Student.ID}', '{entry.Mark}');", con);
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
