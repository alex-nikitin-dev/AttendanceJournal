using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    
    public class Students
    {
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public Group Group
        {
            get;
            set;
        }
        public int Phone
        {
            get;
            set;
        }
        public bool Head
        {
            get;
            set;
        }

    }
    public class Group
    {
        public int ID
        {
            get;
            set;
        }
        public int course
        {
            get;
            set;
        }
        public int group
        {
            get;
            set;
        }

    }
    public class Subject
    {
        public int ID
        {
            get;
            set;
        }
        public string nameofSubject
        {
            get;
            set;
        }

    }
    public class Professor
    {
        public int ID
        {
            get;
            set;
        }
        public string nameOfProfessor
        {
            get;
            set;
        }
        public int phone
        {
            get;
            set;
        }
        public int room
        {
            get;
            set;
        }
    }
    public class Entry
    {
        public DateTime EntryDate
        {
            get;
            set;
        }
        public int NumberOfLesson
        {
            get;
            set;
        }
        public int Room
        {
            get;
            set;
        }
        public Professor Professor
        {
            get;
            set;
        }
        public Subject Subject
        {
            get;
            set;
        }
        public Students Student
        {
            get;
            set;
        }
        public bool Mark
        {
            get;
            set;
        }
    }

}