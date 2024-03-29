﻿using System;
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

        private DrawerLayout drawer;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            //FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            //fab.Click += FabOnClick;

            drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
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
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            Bundle bundle = new Bundle();
            //todo replace to user id
            bundle.PutInt("UserID", 2);

            int id = item.ItemId;

            if (id == Resource.Id.nav_dean_students)
            {
                Intent intent = new Intent(this, typeof(DeanViewStudent));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_dean_journal)
            {
                Intent intent = new Intent(this, typeof(DeanViewJournal));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_dean_subjects)
            {
                Intent intent = new Intent(this, typeof(DeanViewSubject));
                StartActivity(intent);

            }
            else if (id == Resource.Id.nav_dean_professors)
            {
                Intent intent = new Intent(this, typeof(DeanViewProfessor));
                StartActivity(intent);
            }
            else if (id == Resource.Id.nav_gLeader_students)
            {
                Android.Support.V4.App.Fragment fragment = new LeaderStudentsFragment();
                fragment.Arguments = bundle;
                SupportFragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
            }
            else if (id == Resource.Id.nav_gLeader_subjects)
            {
                Android.Support.V4.App.Fragment fragment = new LeaderSubjectsFragment();
                fragment.Arguments = bundle;
                SupportFragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
            }
            else if (id == Resource.Id.nav_gLeader_professors)
            {
                Android.Support.V4.App.Fragment fragment = new LeaderProfessorsFragment();
                fragment.Arguments = bundle;
                SupportFragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
            }
            else if (id == Resource.Id.nav_gLeader_day)
            {
                Android.Support.V4.App.Fragment fragment = new LeaderDayFragment();
                fragment.Arguments = bundle;
                SupportFragmentManager.BeginTransaction()
                              .Replace(Resource.Id.content_frame, fragment)
                              .Commit();
            }
            else if (id == Resource.Id.nav_gLeader_week)
            {
                //There is menu for Group 
            }
            else if (id == Resource.Id.nav_gLeader_semestr)
            {
                //There is menu for Group Leader 
            }

            DrawerLayout drawer = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);
            drawer.CloseDrawer(GravityCompat.Start);
            return true;
        }

        public bool SwitchVisibility()
        {
            NavigationView navigationView = FindViewById<NavigationView>(Resource.Id.nav_view);
            var navSlideShow =  navigationView.Menu.FindItem(Resource.Id.nav_gLeader_week);
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

//if (SwitchVisibility() == false)
//{
//    item.SetTitle("Show What you've just hid");
//}
//else
//{
//    item.SetTitle("Hide it, now!");
//}