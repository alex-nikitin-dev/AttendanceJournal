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
    public class LeaderAddEntryFragment : Android.Support.V4.App.Fragment
    {
        private int UserID;
        private Students user;

        private ListView lvEntry;
        private Spinner spSubject;
        private Spinner spProfessor;
        private Spinner spWeek;
        private TextView tvDate;
        private EditText etNumbOfLesson;
        private EditText etRoom;

        private List<Subject> subjects;
        private List<Professor> professors;
        private List<int> weeks;

        private List<String> subjectsNames;
        private List<String> professorsNames;

        private List<Entry> entries;
        private List<Students> students;
        private ArrayAdapter<String> adapter;
        private DateTime date;
        private int userGroupID;

        private Context context;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_add_entry, container, false);
            lvEntry = root.FindViewById<ListView>(Resource.Id.lv_leader_add_entry);
            tvDate = root.FindViewById<TextView>(Resource.Id.tv_leader_add_entry_date);
            etNumbOfLesson = root.FindViewById<EditText>(Resource.Id.et_leader_add_entry_lesson);
            etRoom = root.FindViewById<EditText>(Resource.Id.et_leader_add_entry_room);
            spSubject = root.FindViewById<Spinner>(Resource.Id.sp_leader_add_entry_subject);
            spWeek = root.FindViewById<Spinner>(Resource.Id.sp_leader_add_entry_week);
            spProfessor = root.FindViewById<Spinner>(Resource.Id.sp_leader_add_entry_professor);
            context = root.Context;

            if (Arguments != null && Arguments.ContainsKey("UserID"))
            {
                UserID = Arguments.GetInt("UserID");
                user = DataBaseHelper.GetStudentByUserID(UserID);
            }
            //userGroupID = user.Group.ID;
            userGroupID = 2;

            date = DateTime.Today;
            tvDate.Text = date.ToShortDateString();
            tvDate.Click += (sender, e) => {
                DatePickerDialog dialog = new DatePickerDialog(root.Context, OnDateSet, date.Year, date.Month-1, date.Day);
                dialog.Show();
            };

            entries = new List<Entry>();
            //todo change to user group id

            students = new List<Students>();
            students = DataBaseHelper.GetListOfStudentsByGroupID(userGroupID);
            List<String> studentNames = new List<String>();
            //students.Add(new Students { ID = 1, Name = "Студент 1", Phone = 380666, Head = false });
            //students.Add(new Students { ID = 2,  Name = "Студент 2", Phone = 380111, Head = false });
            //students.Add(new Students { ID = 2,  Name = "Студент 3", Phone = 380111, Head = false });
            foreach (Students s in students)
                entries.Add(new Entry { Student = s });
            foreach (Students s in students)
                studentNames.Add(s.Name);

            lvEntry.ChoiceMode = Android.Widget.ChoiceMode.Multiple;
            adapter = new ArrayAdapter<String>(root.Context, Android.Resource.Layout.SimpleListItemMultipleChoice, studentNames);
            lvEntry.Adapter = adapter;


            //subjects = DataBaseHelper.GetListOfSubject();
            subjects = new List<Subject>();
            subjects = DataBaseHelper.GetListOfSubject();
            //subjects.Add(new Subject { nameofSubject = "Физика", ID=0 });
            //subjects.Add(new Subject { nameofSubject = "Математика", ID = 1 });
            subjectsNames = new List<String>();
            foreach (Subject s in subjects)
                subjectsNames.Add(s.nameofSubject);

            //professors = DataBaseHelper.GetListOfProfessors();
            professors = new List<Professor>();
            professors = DataBaseHelper.GetListOfProfessors();
            //professors.Add(new Professor { ID=0, room = 510, nameOfProfessor = "Преподаватель 1", phone = 380666 });
            //professors.Add(new Professor { ID=1, room = 310, nameOfProfessor = "Преподаватель 2", phone = 380556 });
            professorsNames = new List<String>();
            foreach (Professor p in professors)
                professorsNames.Add(p.nameOfProfessor);

            weeks = new List<int>();
            for (int i = 1; i < 16; i++)
                weeks.Add(i);

            ArrayAdapter subjectAdapter = new ArrayAdapter<String>(root.Context, Android.Resource.Layout.SimpleSpinnerItem, subjectsNames);
            subjectAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spSubject.Adapter = subjectAdapter;
            ArrayAdapter professorAdapter = new ArrayAdapter<String>(root.Context, Android.Resource.Layout.SimpleSpinnerItem, professorsNames);
            professorAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spProfessor.Adapter = professorAdapter;
            ArrayAdapter weekAdapter = new ArrayAdapter<int>(root.Context, Android.Resource.Layout.SimpleSpinnerItem, weeks);
            weekAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            spWeek.Adapter = weekAdapter;


            return root;
        }
        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            tvDate.Text = e.Date.ToString("dd.MM.yyyy");
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.item_save, menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.menu_item_save)
            {
                saveEntries();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void saveEntries()
        {
            if (etNumbOfLesson.Text.Equals("") || etRoom.Text.Equals(""))
            {
                Toast.MakeText(context, "Fill in all the fields", ToastLength.Long).Show();
                return;
            }

            SparseBooleanArray sbArray = lvEntry.CheckedItemPositions;
            int i = 0;
            foreach (Entry entry in entries){
                entry.EntryDate = DateTime.Parse(tvDate.Text);
                entry.NumberOfLesson = Int32.Parse(etNumbOfLesson.Text);
                entry.Room = Int32.Parse(etRoom.Text);
                entry.Professor = professors.ElementAt(spProfessor.SelectedItemPosition);
                entry.Subject = subjects.ElementAt(spSubject.SelectedItemPosition);
                if (sbArray.Get(i))
                    entry.Mark = 1;
                DataBaseHelper.AddNewEntry(entry);
                i++;
                System.Diagnostics.Debug.WriteLine(entry.Student.Name + " " + entry.Mark);
            }

            Toast.MakeText(context, "New entry saved successfully", ToastLength.Long).Show();
            FragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, new LeaderDayFragment())
                              .Commit();
        }
    }
}