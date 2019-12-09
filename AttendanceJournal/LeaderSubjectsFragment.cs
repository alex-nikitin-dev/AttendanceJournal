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
        private List<String> subjectNameList;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_subjects, container, false);

            lvSubjects = root.FindViewById<ListView>(Resource.Id.lv_leader_subjects);

            subjectNameList = new List<String>();
            List<Subject>  subjectList = DataBaseHelper.GetListOfSubject();
            foreach (Subject s in subjectList)
                subjectNameList.Add(s.nameofSubject);

            //subjectNameList.Add("Физика");
            //subjectNameList.Add("Математика");

            ArrayAdapter<String> adapter = new ArrayAdapter<String>(root.Context, Resource.Layout.leader_list_item_subjects, subjectNameList);

            lvSubjects.Adapter = adapter;

            return root;
        }
    }
}