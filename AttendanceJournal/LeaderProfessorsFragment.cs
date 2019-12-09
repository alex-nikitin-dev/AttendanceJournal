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
    public class LeaderProfessorsFragment : Android.Support.V4.App.Fragment
    {
        private ListView lvProfessors;
        private List<Professor> professorsList;
        ProfessorAdapter adapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View root = inflater.Inflate(Resource.Layout.leader_fragment_professors, container, false);

            lvProfessors = root.FindViewById<ListView>(Resource.Id.lv_leader_professors);

            professorsList = new List<Professor>();
            professorsList = DataBaseHelper.GetListOfProfessors();
            //professorsList.Add(new Professor { room = 510, nameOfProfessor = "Преподаватель 1", phone = 380666 });
            //professorsList.Add(new Professor { room = 310, nameOfProfessor = "Преподаватель 2", phone = 380556 });

            adapter = new ProfessorAdapter(root.Context, professorsList);

            lvProfessors.Adapter = adapter;

            return root;
        }
    }
    public class ProfessorAdapter : BaseAdapter<Professor>
    {
        public List<Professor> professors;
        private Context sContext;
        public ProfessorAdapter(Context context, List<Professor> list)
        {
            professors = list;
            sContext = context;
        }
        public override Professor this[int position]
        {
            get
            {
                return professors[position];
            }
        }
        public override int Count
        {
            get
            {
                return professors.Count;
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
                    view = LayoutInflater.From(sContext).Inflate(Resource.Layout.leader_list_item_professors, parent, false);
                }
                TextView tvName = view.FindViewById<TextView>(Resource.Id.tv_leader_professors_name);
                tvName.Text = professors[position].nameOfProfessor;
                TextView tvPhone = view.FindViewById<TextView>(Resource.Id.tv_leader_professors_phone_number);
                tvPhone.Text = "phone: " + professors[position].phone.ToString();
                TextView tvRoom = view.FindViewById<TextView>(Resource.Id.tv_leader_professors_room);
                tvRoom.Text = "room: " + professors[position].room.ToString();
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