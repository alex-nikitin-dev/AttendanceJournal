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
using Android.Support.V4.App;

namespace AttendanceJournal
{
    public class LeaderStudentsFragment : Android.Support.V4.App.Fragment
    {
        private int UserID;
        private Students user;
        private ListView lvStudents;
        private List<Students> StudentsList;
        LeadStudentsAdapter stAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_students, container, false);
            lvStudents = root.FindViewById<ListView>(Resource.Id.lv_leader_students);

            if (Arguments != null && Arguments.ContainsKey("UserID"))
            {
                UserID = Arguments.GetInt("UserID");
                user = DataBaseHelper.GetStudentByUserID(UserID);
            }

            StudentsList = new List<Students>();
            //todo change to user group id
            StudentsList = DataBaseHelper.GetListOfStudentsByGroupID(2);
            //StudentsList.Add(new Students { Group = 0, Name = "Студент 1", Phone = 380666, Head=false });
            //StudentsList.Add(new Students { Group = 0, Name = "Студент 2", Phone = 380111, Head = false });

            stAdapter = new LeadStudentsAdapter(root.Context, StudentsList);

            lvStudents.Adapter = stAdapter;

            return root;
        }
    }

    public class LeadStudentsAdapter : BaseAdapter<Students>
    {
        public List<Students> students;
        private Context sContext;
        public LeadStudentsAdapter(Context context, List<Students> list)
        {
            students = list;
            sContext = context;
        }
        public override Students this[int position]
        {
            get
            {
                return students[position];
            }
        }
        public override int Count
        {
            get
            {
                return students.Count;
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
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_students, parent, false);
                }
                TextView tvName = view.FindViewById<TextView>(Resource.Id.tv_leader_students_name);
                tvName.Text = students[position].Name;
                TextView tvPhone = view.FindViewById<TextView>(Resource.Id.tv_leader_students_phone);
                tvPhone.Text = students[position].Group.ID.ToString();
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