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
        private ListView lvStudents;
        private List<Student> studentList;
        LeadStudentAdapter stAdapter;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_students, container, false);

            lvStudents = root.FindViewById<ListView>(Resource.Id.lv_leader_students);

            studentList = new List<Student>();
            //todo change to user group id
            //studentList = DataBaseHelper.GetListOfStudents(0);
            studentList.Add(new Student { id = 0, groupId = 0, name = "Студент 1", phone = 380666 });
            studentList.Add(new Student { id = 0, groupId = 0, name = "Студент 2", phone = 380556 });

            stAdapter = new LeadStudentAdapter(root.Context, studentList);

            lvStudents.Adapter = stAdapter;

            return root;
        }
    }

    public class Student
    {
        public int id
        {
            get;
            set;
        }
        public String name
        {
            get;
            set;
        }
        public int groupId
        {
            get;
            set;
        }
        public int phone
        {
            get;
            set;
        }
    }

    public class LeadStudentAdapter : BaseAdapter<Student>
    {
        public List<Student> students;
        private Context sContext;
        public LeadStudentAdapter(Context context, List<Student> list)
        {
            students = list;
            sContext = context;
        }
        public override Student this[int position]
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
                tvName.Text = students[position].name;
                TextView tvPhone = view.FindViewById<TextView>(Resource.Id.tv_leader_students_phone);
                tvPhone.Text = students[position].phone.ToString();
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