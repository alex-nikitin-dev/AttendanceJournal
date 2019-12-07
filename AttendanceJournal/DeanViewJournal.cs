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
        StudentAdapter stAdapter;

        private ListView grouplistView;
        private List<Group> grouplist;
        GroupAdapter groupAdapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.dean_view_group);
            grouplistView = FindViewById<ListView>(Resource.Id.dean_group_viewList);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_add_group);
            if (fab.IsShown)
            {
                fab.Hide();
            }

            grouplist = new List<Group>();
            grouplist = DataBaseHelper.GetListOfGroup();

            groupAdapter = new GroupAdapter(this, grouplist);

            grouplistView.Adapter = groupAdapter;
            grouplistView.ItemClick += GrouplistView_ItemClick;

        }
        private void GrouplistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SetContentView(Resource.Layout.dean_view_student);
            studentlistView = FindViewById<ListView>(Resource.Id.demolist);
            studentList = new List<Students>();
            var selectcorse = grouplist[e.Position].course;
            var selectgroup = grouplist[e.Position].group;
            var groupId = DataBaseHelper.GetGroupIDByCorseAndNumber(selectcorse, selectgroup);
            studentList = DataBaseHelper.GetListOfStudentsByGroupID(groupId);
            stAdapter = new StudentAdapter(this, studentList);
            studentlistView.Adapter = stAdapter;
 
        }
       
    }
}