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
        private int userGroupID;
        private Students user;
        private ListView lvEntry;
        private TextView tvLessonNumb;
        private TextView tvDate;
        private TextView tvSubject;
        private TextView tvProfessor;
        private TextView tvRoom;
        private Entry mainEntry;
        private List<Entry> entries;
        private DateTime date;
        private int numberOfLesson;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_lesson_details, container, false);
            lvEntry = root.FindViewById<ListView>(Resource.Id.lv_leader_add_entry);
            tvLessonNumb = root.FindViewById<TextView>(Resource.Id.tv_leader_lesson_number);
            tvDate = root.FindViewById<TextView>(Resource.Id.tv_leader_lesson_date);
            tvSubject = root.FindViewById<TextView>(Resource.Id.tv_leader_lesson_subject);
            tvProfessor = root.FindViewById<TextView>(Resource.Id.tv_leader_lesson_professor);
            tvRoom = root.FindViewById<TextView>(Resource.Id.tv_leader_lesson_room);

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
            userGroupID = 2;

            mainEntry = DataBaseHelper.GetEntryDetailsByLessonNumbAndDate(userGroupID, numberOfLesson, date);
            entries = DataBaseHelper.GetListOfLessonDelailEntriesByLessonNumbAndDate(userGroupID, numberOfLesson, date);
            //entries.Add(new Entry { EntryDate = date, NumberOfLesson = 2, Room = 500, Professor = new Professor { nameOfProfessor = "professor1" }, Subject = new Subject { nameofSubject = "subj" }, Student=new Students { Name="stud" } , Mark=0});

            tvLessonNumb.Text = mainEntry.NumberOfLesson.ToString();
            tvDate.Text = mainEntry.EntryDate.ToString("dd.MM.yyyy");
            tvSubject.Text = mainEntry.Subject.nameofSubject;
            tvProfessor.Text = mainEntry.Professor.nameOfProfessor.ToString();
            tvRoom.Text = mainEntry.Room.ToString();

            List<String> studentNames = new List<String>();
            //students.Add(new Students { ID = 1,Name = "Студент 1", Phone = 380666, Head = false });
            //students.Add(new Students { ID = 2,Name = "Студент 2", Phone = 380111, Head = false });
            //students.Add(new Students { ID = 2, Name = "Студент 3", Phone = 380111, Head = false });


            LeadEntryReadOnlyAdapter adapter = new LeadEntryReadOnlyAdapter(root.Context, entries);
            lvEntry.Adapter = adapter;

            return root;
        }
    }
    public class LeadEntryReadOnlyAdapter : BaseAdapter<Entry>
    {
        public List<Entry> entries;
        private Context sContext;
        public LeadEntryReadOnlyAdapter(Context context, List<Entry> list)
        {
            entries = list;
            sContext = context;
        }
        public override Entry this[int position]
        {
            get
            {
                return entries[position];
            }
        }
        public override int Count
        {
            get
            {
                return entries.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            try
            {
                if (view == null)
                {
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_entry_readonly, parent, false);
                }
                TextView tvName = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_name_readonly);
                tvName.Text = entries[position].Student.Name;
                CheckBox cbMark = view.FindViewById<CheckBox>(Resource.Id.tv_leader_entry_mark_readonly);
                cbMark.Checked = entries[position].Mark.Equals(1);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return view;
        }
    }
}