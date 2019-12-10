﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    public class LeaderDayFragment : Android.Support.V4.App.Fragment
    {
        private int UserID;
        private Students user;
        private Button btnLeft;
        private Button btnRight;
        private ListView lvEntry;
        private List<Entry> entries;
        private LeaderEntryAdapter adapter;
        private DateTime date;

        private int userGroupID;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_day, container, false);
            lvEntry = root.FindViewById<ListView>(Resource.Id.lv_leader_day_entry);
            btnLeft = root.FindViewById<Button>(Resource.Id.btn_leader_day_left);
            btnRight = root.FindViewById<Button>(Resource.Id.btn_leader_day_right);

            if (Arguments != null && Arguments.ContainsKey("UserID"))
            {
                UserID = Arguments.GetInt("UserID");
                user = DataBaseHelper.GetStudentByUserID(UserID);
            }
            //userGroupID = user.Group.ID;
            userGroupID = 2;

            entries = new List<Entry>();
            date = DateTime.Today;
            entries = DataBaseHelper.GetListOfDayEntriesByGroupIDAndDate(userGroupID, date);
            //foreach(Entry entry in entries)
            //    System.Diagnostics.Debug.WriteLine(entry.Professor.nameOfProfessor);
            //entries.Add(new Entry { EntryDate=date, NumberOfLesson=2, Room=500, Professor=new Professor { },Subject=new Subject { }, Student=new Students { } });
            //entries.Add(new Students { Group = 0, Name = "Студент 2", Phone = 380111, Head = false });

            adapter = new LeaderEntryAdapter(root.Context, entries);
            lvEntry.Adapter = adapter;

            FloatingActionButton fab = root.FindViewById<FloatingActionButton>(Resource.Id.fab_leader_add_entry);
            fab.Click += FabOnClick;

            return root;
        }
        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            FragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, new LeaderAddEntryFragment())
                              .Commit();
        }
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.item_details, menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.menu_item_details)
            {
                Bundle bundle = new Bundle();
                bundle.PutInt("UserID", UserID);
                Android.Support.V4.App.Fragment fragment = new LeaderDayDetailsFragment();
                FragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        public void updateEntries()
        {
            date = date.AddDays(-1);
            entries = new List<Entry>();
            entries = DataBaseHelper.GetListOfDayEntriesByGroupIDAndDate(userGroupID, date);
            adapter.NotifyDataSetChanged();
        }
    }

    public class LeaderEntryAdapter : BaseAdapter<Entry>
    {
        public List<Entry> entries;
        private Context sContext;
        public LeaderEntryAdapter(Context context, List<Entry> list)
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
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_entry, parent, false);
                }
                TextView tvName = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_name);
                tvName.Text = entries[position].Student.Name;
                TextView tvPasses = view.FindViewById<TextView>(Resource.Id.tv_leader_entry_passes);
                //tvPasses.Text = entries[position].Passes.ToString();
                System.Diagnostics.Debug.WriteLine(entries[position].Student.Name + " " + entries[position].Mark);
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