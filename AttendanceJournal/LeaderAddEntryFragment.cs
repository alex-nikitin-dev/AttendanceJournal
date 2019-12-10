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
        private LeaderEditableEntryAdapter adapter;
        private DateTime date;
        //private int userGroupID;
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

            
            date = DateTime.Now;
            tvDate.Text = date.ToShortDateString();
            tvDate.Click += (sender, e) => {
                DatePickerDialog dialog = new DatePickerDialog(root.Context, OnDateSet, date.Year, date.Month-1, date.Day);
                dialog.Show();
            };

            entries = new List<Entry>();
            //todo change to user group id
            //userGroupID = 0;

            //students = DataBaseHelper.GetListOfStudentsByGroupID(userGroupID);
            students = new List<Students>();
            foreach(Students s in students)
                entries.Add(new Entry { Student = s });

            adapter = new LeaderEditableEntryAdapter(root.Context, entries);
            lvEntry.Adapter = adapter;


            //subjects = DataBaseHelper.GetListOfSubject();
            subjects = new List<Subject>();
            subjectsNames = new List<String>();
            foreach (Subject s in subjects)
                subjectsNames.Add(s.nameofSubject);

            //professors = DataBaseHelper.GetListOfProfessors();
            professors = new List<Professor>();
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
            tvDate.Text = e.Date.ToLongDateString();
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
                return;

            SparseBooleanArray sbArray = lvEntry.CheckedItemPositions;
            int i = 0;
            foreach (Entry entry in entries){
                entry.EntryDate = DateTime.Parse(tvDate.Text);
                entry.NumberOfLesson = Int32.Parse(etNumbOfLesson.Text);
                entry.Room = Int32.Parse(etRoom.Text);
                entry.Professor = professors.ElementAt(spProfessor.SelectedItemPosition);
                entry.Subject = subjects.ElementAt(spSubject.SelectedItemPosition);
                if (sbArray.Get(i))
                    entry.Mark = true;
                //DataBaseHelper.AddNewEntry(entry);
            }
        }
    }

    public class LeaderEditableEntryAdapter : BaseAdapter<Entry>
    {
        public List<Entry> entries;
        private Context sContext;
        public LeaderEditableEntryAdapter(Context context, List<Entry> list)
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
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_entry_editable, parent, false);
                }
                TextView tvName = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_name_editable);
                tvName.Text = entries[position].EntryDate.ToString();
                CheckBox cbMark = view.FindViewById<CheckBox>(Resource.Id.cb_leader_entry_mark_editable);
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