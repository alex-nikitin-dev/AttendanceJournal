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
    [Activity(Label = "DeanViewSubject")]
    public class DeanViewSubject : Activity
    {
        private ListView subjectlistView;
        private List<Subject> subgectList;
        SubjectAdapter subjectAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dean_view_subject);
            subjectlistView = FindViewById<ListView>(Resource.Id.dean_group_viewList);
            subgectList = new List<Subject>();
            subgectList = DataBaseHelper.GetListOfSubject();
            subjectAdapter = new SubjectAdapter(this, subgectList);
            subjectlistView.Adapter = subjectAdapter;
            subjectlistView.ItemClick += SubjectlistView_ItemClick;
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_add_subject);
            fab.Click += Fab_AddNewSubject;
        }

        private void Fab_AddNewSubject(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.dean_add_subject);
            Button addSubject = FindViewById<Button>(Resource.Id.dean_newSubject_button_Add);
            addSubject.Click += AddSubject_Click;
        }

        private void AddSubject_Click(object sender, EventArgs e)
        {
            string name = FindViewById<EditText>(Resource.Id.dean_newSubject_name).Text;
            if (DataBaseHelper.IsServerAlive())
            {
                DataBaseHelper.AddNewSubgect(name);
                Toast.MakeText(this, "New subject is added!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Server isn't alive", ToastLength.Long).Show();
            }
            Finish();
        }

        private void SubjectlistView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
    public class SubjectAdapter : BaseAdapter<Subject>
    {
        public List<Subject> gList;
        private Context sContext;
        public SubjectAdapter(Context context, List<Subject> list)
        {
            gList = list;
            sContext = context;
        }

        public override Subject this[int position]
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_content_list_subject, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.deanTextGroup);
                txtName.Text = gList[position].nameofSubject.ToString();
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