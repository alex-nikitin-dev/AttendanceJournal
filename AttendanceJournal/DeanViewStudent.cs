using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    [Activity(Label = "DeanViewStudent")]
    public class DeanViewStudent : Activity
    {
        private ListView studentlistView;
        private List<Students> mlist;
        StudentAdapter stAdapter;

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
            fab.Click += FabAddGroupOnClick;
            
        }

        private void FabAddGroupOnClick(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.dean_add_new_group);
            Button addGroiup = FindViewById<Button>(Resource.Id.dean_newStd_Button_Add);
            addGroiup.Click += ButtonAddGroup;
        }
        private void ButtonAddGroup(object sender, EventArgs e)
        {
            int course = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_Course).Text);
            int groupNumber = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_GroupNumber).Text);
            int year = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_EntryYear).Text);

            if (DataBaseHelper.IsServerAlive())
            {
                if (DataBaseHelper.GetGroupIDByNumberAndYear(groupNumber, year) == -1)
                {
                    DataBaseHelper.AddNewGroup(course, groupNumber, year);
                    Toast.MakeText(this, "New group is added!", ToastLength.Long).Show();
                }
                else
                {
                    Toast.MakeText(this, "That group is exist already", ToastLength.Long).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Server isn't alive", ToastLength.Long).Show();
            }
            Finish();

        }

        private void GrouplistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            SetContentView(Resource.Layout.dean_view_student);
            FloatingActionButton fabAddStd = FindViewById<FloatingActionButton>(Resource.Id.fab_add_student);
            fabAddStd.Click += FabAddStd_Click;

            studentlistView = FindViewById<ListView>(Resource.Id.demolist);
            mlist = new List<Students>();
            var selectcorse = grouplist[e.Position].course;
            var selectgroup = grouplist[e.Position].group;
            var groupId = DataBaseHelper.GetGroupIDByCorseAndNumber(selectcorse, selectgroup);
            mlist = DataBaseHelper.GetListOfStudentsByGroupID(groupId);
            
            stAdapter = new StudentAdapter(this, mlist);
            studentlistView.Adapter = stAdapter;
            studentlistView.ItemClick += StudentlistView_ItemClick;
        }

        private void FabAddStd_Click(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.dean_add_new_studend);
            Button addStd = FindViewById<Button>(Resource.Id.dean_newStd_Button_Add);
            addStd.Click += AddStd_Click;
        }

        private void AddStd_Click(object sender, EventArgs e)
        {
            string name = FindViewById<EditText>(Resource.Id.dean_newStd_Name).Text;
            int phone = int.Parse(FindViewById<EditText>(Resource.Id.dean_newStd_Phone).Text);
            int group = int.Parse(FindViewById<EditText>(Resource.Id.dean_newStd_GroupNumber).Text);
            int year = int.Parse(FindViewById<EditText>(Resource.Id.dean_newStd_StartYear).Text);
            int groupId = DataBaseHelper.GetGroupIDByNumberAndYear(group, year);
            Toast.MakeText(Application.Context, name + " " + phone + " " + group + " " + year, ToastLength.Long).Show();

            if (DataBaseHelper.IsServerAlive())
            {
                DataBaseHelper.AddNewStudent(name, groupId, phone);
                Toast.MakeText(this, "New student is added!", ToastLength.Long).Show();
                //StartActivity(typeof(MainActivity));
            }
            else
            {
                Toast.MakeText(this, "Server isn't alive", ToastLength.Long).Show();
            }
            Finish();
        }
    

        private void StudentlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var select = mlist[e.Position].Name;
            Toast.MakeText(this, select, ToastLength.Long).Show();
        }
    }
   
    
    public class Group
    {
     public int course
        {
            get;
            set;
        }
        public int group
        {
            get;
            set;
        }

    }
    public class GroupAdapter : BaseAdapter<Group>
    {
        public List<Group> gList;
        private Context sContext;
        public GroupAdapter(Context context, List<Group> list)
        {
            gList = list;
            sContext = context;
        }

        public override Group this[int position]
        {
            get
            {
                return gList[position];
            }
        }
        public override int Count
        {
            get
            {
                return gList.Count;
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_content_list_group, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.deanTextGroup);
                txtName.Text = gList[position].course.ToString()+gList[position].group.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            finally { }
            return row;
        }
    }
    public class Students
    {
        public string Name
        {
            get;
            set;
        }
        public int Group
        {
            get;
            set;
        }
        public int Phone
        {
            get;
            set;
        }
        public bool Head
        {
            get;
            set;
        }
        
    }
    public class StudentAdapter : BaseAdapter<Students>
    {
        public List<Students> sList;
        private Context sContext;
        public StudentAdapter(Context context, List<Students> list)
        {
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
                row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_content_list_student, null, false);
                }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.Name);
                txtName.Text = sList[position].Name+" "+sList[position].Phone;
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