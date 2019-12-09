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
    public class LeaderSubjectsFragment : Android.Support.V4.App.Fragment
    {
        private ListView lvSubjects;
        private List<String> subjectList;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_subjects, container, false);

            lvSubjects = root.FindViewById<ListView>(Resource.Id.lv_leader_subjects);

            subjectList = new List<String>();
            //subjectList = DataBaseHelper.GetListOfSubjects();
            subjectList.Add("Физика");
            subjectList.Add("Математика");

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(root.Context, Resource.Layout.leader_list_item_subjects, subjectList);

            lvSubjects.Adapter = adapter;

            return root;
        }
    }
}