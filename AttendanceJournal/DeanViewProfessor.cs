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
    [Activity(Label = "DeanViewProfessor")]
    public class DeanViewProfessor : Activity
    {
        private ListView professorListView;
        private List<Professor> professorList;
        ProfessorAdaptor professorAdaptor;
        //private ListView studentlistView;
        //private List<Students> mlist;
        //StudentAdapter stAdapter;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dean_view_professor);
            professorListView = FindViewById<ListView>(Resource.Id.dean_professor_viewList);
            professorList = new List<Professor>();
            professorList = DataBaseHelper.GetListOfProfessors();
            professorAdaptor = new ProfessorAdaptor(this,professorList);
            professorListView.Adapter = professorAdaptor;
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab_add_professor);
            fab.Click += Fab_AddProfessorClick;
        }

        private void Fab_AddProfessorClick(object sender, EventArgs e)
        {
            SetContentView(Resource.Layout.dean_add_new_professor);
            Button addProfessor = FindViewById<Button>(Resource.Id.dean_newProfessor_button_add);
            addProfessor.Click += AddProfessor_Click;
        }

        private void AddProfessor_Click(object sender, EventArgs e)
        {
            string name = FindViewById<EditText>(Resource.Id.dean_newProfessor_Name).Text;
            int phone = int.Parse(FindViewById<EditText>(Resource.Id.dean_newProfessor_phone).Text);
            int room = int.Parse(FindViewById<EditText>(Resource.Id.dean_newProfessor_room).Text);
            if (DataBaseHelper.IsServerAlive())
            {
                DataBaseHelper.AddNewProfessor(name,phone,room);
                Toast.MakeText(this, "New professor is added!", ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Server isn't alive", ToastLength.Long).Show();
            }
            Finish();
        }
    }
    public class ProfessorAdaptor : BaseAdapter<Professor>
    {
        public List<Professor> gList;
        private Context sContext;
        public ProfessorAdaptor(Context context, List<Professor> list)
        {
            gList = list;
            sContext = context;
        }
        public override Professor this[int position]
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
                    row = LayoutInflater.From(sContext).Inflate(Resource.Layout.dean_content_list_professor, null, false);
                }
                TextView txtName = row.FindViewById<TextView>(Resource.Id.deanTextProfessor);
                txtName.Text = gList[position].nameOfProfessor.ToString() +"\n room "+ gList[position].room.ToString()+"\n phone 0"+ gList[position].phone.ToString();
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