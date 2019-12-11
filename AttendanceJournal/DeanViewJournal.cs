using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    [Activity(Label = "Dean_View_Journal")]
    public class DeanViewJournal : Activity
    {
        private ListView studentlistView;
        private List<Students> studentList;
        private List<Mark> marksList;
        Journal_StudentAdapter stAdapter;

        private ListView grouplistView;
        private List<Group> grouplist;
        GroupAdapter groupAdapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.dean_view_group);
            grouplistView = FindViewById<ListView>(Resource.Id.dean_group_viewList);          
            grouplist = new List<Group>();  
            grouplist = DataBaseHelper.GetListOfGroup();
            groupAdapter = new GroupAdapter(this, grouplist);
            grouplistView.Adapter = groupAdapter;
            grouplistView.ItemClick += GrouplistView_ItemClick;
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_add_group);
            fab.Hide();

        }
        private void GrouplistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SetContentView(Resource.Layout.dean_view_journal_student);
            studentlistView = FindViewById<ListView>(Resource.Id.j_demolist);
            studentList = new List<Students>();
            var selectcorse = grouplist[e.Position].course;
            var selectgroup = grouplist[e.Position].group;
            var groupId = DataBaseHelper.GetGroupIDByCorseAndNumber(selectcorse, selectgroup);
            studentList = DataBaseHelper.GetListOfStudentsByGroupID(groupId);
            marksList = new List<Mark>();
            marksList = DataBaseHelper.GetListOfCountMarksByStudentID(studentList);
            stAdapter = new Journal_StudentAdapter(this, studentList, marksList);
            studentlistView.Adapter = stAdapter;
            studentlistView.ItemClick += StudentlistView_ItemClick;
        }
        private void StudentlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var select = studentList[e.Position].Name;
            Toast.MakeText(this, select, ToastLength.Long).Show();
        }
    }
    public class Journal_StudentAdapter : BaseAdapter<Students>
    {
        public List<Students> sList;
        public List<Mark> mList;
        private Context sContext;
        public Journal_StudentAdapter(Context context, List<Students> list, List<Mark> marks)
        {
            mList = marks;
            sList = list;
            sContext = context;
        }
        public override Students this[int position]
        {
            get
            {
                return sList[position];
            }
        }
        public override int Count
        {
            get
            {
                return sList.Count;
            }
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;
            try
            {
                if (row == null)
                {
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_content_list_journal_student, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.j_Name);
                TextView txtMark = row.FindViewById<TextView>(Resource.Id.j_Mark);
                txtName.Text = sList[position].Name;
                txtMark.Text = "count = "+mList[position].mark.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }
    }
}