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
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Data;
namespace AttendanceJournal
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme.Login", MainLauncher = true)]
    public class LoginActivity : Activity
    {
        EditText email;
        EditText password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login);

            email = FindViewById<EditText>(Resource.Id.txtEmail);
            password = FindViewById<EditText>(Resource.Id.txtPassword);
            //Trigger click event of Login Button  
            var button = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.btnLogin);
            button.Click += DoLogin;

            StartActivity(typeof(MainActivity));
        }

        public void DoLogin(object sender, EventArgs e)
        {
            try
            {
                var pwdHash = HashSHA512(password.Text);
                var pwdHashOrigin = DataBaseHelper.GetUserPwdHash(email.Text);

                if (!String.IsNullOrEmpty(pwdHashOrigin) &&
                    pwdHashOrigin == pwdHash)
                {
                    Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
                    StartActivity(typeof(MainActivity));
                }
                else
                {
                    Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
                }
            }
            catch (MySqlException)
            {

                Toast.MakeText(this, "Could not connect to database server", ToastLength.Long).Show();
            }
           
        }
        public string HashSHA512(string value)
        {
            using (var sha = SHA512.Create())
            {
                return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(value)));
            }
        }
    }
}