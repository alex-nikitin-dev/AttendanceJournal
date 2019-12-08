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
        public string Name
        {
            get;
            set;
        }
        public int Group
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
        public string nameofSubject
        {
            get;
            set;
        }

    }
    public class Professor
    {
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
}