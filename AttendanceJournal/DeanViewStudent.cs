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
            //List<Group> objgroup = new List<Group>();
            //objgroup.Add(new Group { group = 1151});
            //objgroup.Add(new Group { group = 2151 });
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
            Button addGroiup = FindViewById<Button>(Resource.Id.dean_addGroup_AddButton);
            addGroiup.Click += ButtonAddGroup;
        }
        private void ButtonAddGroup(object sender, EventArgs e)
        {
            int course = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_Course).Text);
            int groupNumber = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_GroupNumber).Text);
            int year = int.Parse(FindViewById<EditText>(Resource.Id.dean_addGroup_EntryYear).Text);

            if (DataBaseHelper.IsServerAlive())
            {
                if (DataBaseHelper.GetGroupID(groupNumber, year) == -1)
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
            List<Students> objstud = new List<Students>();
            objstud.Add(new Students
            {
                Name = "Suresh",
                Age = 26
            });
            objstud.Add(new Students
            {
                Name = "C#Cornet",
                Age = 26
            });
            studentlistView = FindViewById<ListView>(Resource.Id.demolist);
            mlist = new List<Students>();
            mlist = objstud;
            stAdapter = new StudentAdapter(this, mlist);
            studentlistView.Adapter = stAdapter;
            studentlistView.ItemClick += StudentlistView_ItemClick;


            //var select = grouplist[e.Position].group;
            //var s2 = mlist[e.Position].Name + " " + mlist[e.Position].Age;
            //Toast.MakeText(this, s2, ToastLength.Long).Show();
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_view_student, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.Name);
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
    public int Age
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
                row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_view_student, null, false);
                }
            TextView txtName = row.FindViewById<TextView>(Resource.Id.Name);
                txtName.Text = sList[position].Name +" "+ sList[position].Age;
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