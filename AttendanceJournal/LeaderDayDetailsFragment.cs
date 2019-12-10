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
    public class LeaderDayDetailsFragment : Android.Support.V4.App.Fragment
    {
        private int UserID;
        private Students user;
        private ListView lvEntry;
        private List<Entry> entries;
        private LeaderEntryDetailsAdapter adapter;
        private DateTime date;
        private int userGroupID;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_day_details, container, false);
            lvEntry = root.FindViewById<ListView>(Resource.Id.lv_leader_day_details);

            if (Arguments != null && Arguments.ContainsKey("UserID"))
            {
                UserID = Arguments.GetInt("UserID");
                user = DataBaseHelper.GetStudentByUserID(UserID);
            }

            entries = new List<Entry>();
            //todo change to user group id
            userGroupID = 0;
            date = DateTime.Now;
            //entries = DataBaseHelper.GetListOfDayDelailEntriesByGroupIDAndDate(userGroupID, date);
            entries.Add(new Entry { EntryDate = date, NumberOfLesson = 2, Room = 500, Professor = new Professor {nameOfProfessor="professor1" }, Subject = new Subject {nameofSubject="subj" } });
            

            adapter = new LeaderEntryDetailsAdapter(root.Context, entries);
            lvEntry.Adapter = adapter;
            lvEntry.ItemClick += (s, e) =>
            {
                int p = e.Position;

                Bundle bundle = new Bundle();
                bundle.PutInt("UserID", UserID);
                bundle.PutInt("NumberOfLesson", entries[p].NumberOfLesson);
                bundle.PutString("Date", entries[p].EntryDate.ToString());
                Android.Support.V4.App.Fragment fragment = new LeaderLessonDetailsFragment();
                FragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
            };

            return root;
        }
    }

    public class LeaderEntryDetailsAdapter : BaseAdapter<Entry>
    {
        public List<Entry> entries;
        private Context sContext;
        public LeaderEntryDetailsAdapter(Context context, List<Entry> list)
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
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_entry_details, parent, false);
                }
                TextView tvLessonNumb = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_detail_lesson_numb);
                tvLessonNumb.Text = entries[position].NumberOfLesson.ToString();
                TextView tvSubject = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_detail_subject);
                tvSubject.Text = entries[position].Subject.nameofSubject;
                TextView tvProfessor = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_detail_professor);
                tvProfessor.Text = entries[position].Professor.nameOfProfessor;
                TextView tvRoom = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_detail_room);
                tvRoom.Text = "room " + entries[position].Room.ToString();
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