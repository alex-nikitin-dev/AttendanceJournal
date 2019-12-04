using System;
using Android;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MySql.Data.MySqlClient;
using System.Data;
using static Android.App.ActionBar;
using Android.Content;

namespace AttendanceJournal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar")]
    public class MainActivity : AppCompatActivity, NavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            ActionBarDrawerToggle toggle = new ActionBarDrawerToggle(this, drawer, toolbar, Resource.String.navigation_drawer_open, Resource.String.navigation_drawer_close);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();

            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            navigationView.SetNavigationItemSelectedListener(this);
        }

        public override void OnBackPressed()
        {
            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            if(drawer.IsDrawerOpen(GravityCompat.Start))
            {
                drawer.CloseDrawer(GravityCompat.Start);
            }
            else
            {
                base.OnBackPressed();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View) sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
       
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            int id = item.ItemId;

            if (id == Resource.Id.nav_camera)
            {


                Intent intent = new Intent(this,typeof(AddNewStudent));
                StartActivity(intent);

                //try
                //{
                //    //MySqlConnection mySqlConnection = new MySqlConnection("Server=192.168.1.100;database=JournalDB;User Id=journaluser;Password=J1r5_jOngksnL_n8l11!;charset=utf8");
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
            else if (id == Resource.Id.nav_gallery)
            {
                Intent intent = new Intent(this, typeof(DeanViewStudent));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_slideshow)
            {

            }
            else if (id == Resource.Id.nav_manage)
            {

            }
            else if (id == Resource.Id.nav_share)
            {
                if (SwitchVisibility() == false)
                {
                    item.SetTitle("Show What you've just hid");
                }
                else
                {
                    item.SetTitle("Hide it, now!");
                }
            }
            else if (id == Resource.Id.nav_send)
            {

            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public bool SwitchVisibility()
        {
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            var navSlideShow =  navigationView.Menu.FindItem(Resource.Id.nav_slideshow);
            navSlideShow.SetVisible(navSlideShow.IsVisible ? false : true);
            return navSlideShow.IsVisible;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

