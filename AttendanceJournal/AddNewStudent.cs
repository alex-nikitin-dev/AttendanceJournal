﻿using System;
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
    public class AddNewStudent : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            //base.OnCreate(savedInstanceState);
            //SetContentView(Resource.Layout.dean_add_new_studend);
            //Button addStd = FindViewById<Button>(Resource.Id.dean_newStd_Button_Add);
            //addStd.Click += AddStd_Click;

            // Create your application here
        }
        private void AddStd_Click(object sender, EventArgs e)
        {
            //string name = FindViewById<EditText>(Resource.Id.dean_newStd_Name).Text;
            //int phone = int.Parse( FindViewById<EditText>(Resource.Id.dean_newStd_Phone).Text);
            //int group = int.Parse(FindViewById<EditText>(Resource.Id.dean_newStd_GroupNumber).Text);
            //int year = int.Parse(FindViewById<EditText>(Resource.Id.dean_newStd_StartYear).Text);
            //Toast.MakeText(Application.Context, name+" "+phone+" "+group+" "+year, ToastLength.Long).Show();
            
            //if (DataBaseHelper.IsServerAlive())
            //{
            //    DataBaseHelper.AddNewStudent(name,group,year,phone);
            //    Toast.MakeText(this, "New student is added!", ToastLength.Long).Show();
            //    //StartActivity(typeof(MainActivity));
            //}
            //else
            //{
            //    Toast.MakeText(this, "Server isn't alive", ToastLength.Long).Show();
            //}
            //Finish(); 
        }
    }
}
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