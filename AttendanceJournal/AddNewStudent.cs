using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AttendanceJournal
{
    [Activity(Label = "AddNewStudent")]
    public class AddNewStudent : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.dean_add_new_student);
            Button addStd = FindViewById<Button>(Resource.Id.dean_newStd_Button_Add);
            addStd.Click += AddStd_Click;

            // Create your application here
        }
        private void AddStd_Click(object sender, EventArgs e)
        {
            EditText name = FindViewById<EditText>(Resource.Id.dean_newStd_Name);
            string name1 = name.Text;
            Toast.MakeText(Application.Context, name1, ToastLength.Long).Show();
            Finish();

            //try
            //{
            //    MySqlConnection mySqlConnection = new MySqlConnection("Server=specowka.ddns.net; Port=13110;database=JournalDB;User Id=journaluser;Password=J1r5_jOngksnL_n8l11!;charset=utf8");
            //    if (mySqlConnection.State == ConnectionState.Closed)
            //    {
            //        mySqlConnection.Open();

            //        MySqlCommand mySqlCommand = new MySqlCommand(
            //            "INSERT INTO UserGroup (GroupName) " +
            //            "VALUES ('1151')",
            //            mySqlConnection);
            //        mySqlCommand.ExecuteNonQuery();
            //        mySqlConnection.Close();

            //        Toast.MakeText(Application.Context, "OK", ToastLength.Long).Show();
            //    }
            //}
            //catch (Exception e)
            //{
            //    Toast.MakeText(Application.Context, e.Message, ToastLength.Long).Show();
            //}
        }
    }
}