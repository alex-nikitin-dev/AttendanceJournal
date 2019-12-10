using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    public class LeaderLessonDetailsFragment : Android.Support.V4.App.Fragment
    {
        private int UserID;
        private Students user;
        private int userGroupID;
        private ListView lvEntry;
        private List<Entry> entries;
        private DateTime date;
        private int numberOfLesson;
        private List<Students> students;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_lesson_details, container, false);
            lvEntry = root.FindViewById<ListView>(Resource.Id.lv_leader_add_entry);

            if (Arguments != null)
            {
                if (Arguments.ContainsKey("UserID"))
                {
                    UserID = Arguments.GetInt("UserID");
                    user = DataBaseHelper.GetStudentByUserID(UserID);
                }
                if (Arguments.ContainsKey("Date"))
                {
                    date = DateTime.Parse(Arguments.GetString("Date"));
                }
                if (Arguments.ContainsKey("NumberOfLesson"))
                {
                    numberOfLesson = Arguments.GetInt("NumberOfLesson");
                }
            }

            entries = new List<Entry>();
            //todo change to user group id
            userGroupID = 0;
            date = DateTime.Now;
            entries = DataBaseHelper.GetListOfDayDelailEntriesByGroupIDAndDate(userGroupID, date);
            entries.Add(new Entry { EntryDate = date, NumberOfLesson = 2, Room = 500, Professor = new Professor { nameOfProfessor = "professor1" }, Subject = new Subject { nameofSubject = "subj" }, Student=new Students { Name="stud" } , Mark=0});


            students = new List<Students>();
            List<String> studentNames = new List<String>();
            //students.Add(new Students { ID = 1,Name = "Студент 1", Phone = 380666, Head = false });
            //students.Add(new Students { ID = 2,Name = "Студент 2", Phone = 380111, Head = false });
            //students.Add(new Students { ID = 2, Name = "Студент 3", Phone = 380111, Head = false });
            foreach (Students s in students)
                entries.Add(new Entry { Student = s });
            foreach (Students s in students)
                studentNames.Add(s.Name);


            ArrayAdapter<String> adapter = new ArrayAdapter<String>(root.Context, Android.Resource.Layout.SimpleListItemMultipleChoice, studentNames);
            lvEntry.Adapter = adapter;

            return root;
        }
    }
}