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
        }

        public void DoLogin(object sender, EventArgs e)
        {
            var pwdHash = HashSHA512(password.Text);
            var pwdHashOrigin = HashSHA512("12345");

            if (email.Text == "main@gmail.com" && pwdHash == pwdHashOrigin)
            {
                Toast.MakeText(this, "Login successfully done!", ToastLength.Long).Show();
                StartActivity(typeof(MainActivity));
            }
            else
            {  
                Toast.MakeText(this, "Wrong credentials found!", ToastLength.Long).Show();
            }
        }
        public static string HashSHA512(string value)
        {
            using (var sha = SHA512.Create())
            {
                return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(value)));
            }
        }
    }
}