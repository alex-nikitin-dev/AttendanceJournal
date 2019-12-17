using System;

using Android.App;
using Android.OS;
using Android.Widget;

namespace AttendanceJournal
{
    [Activity(Label = "ServerPrefs")]
    public class ServerPrefs : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.server_prefs);
            ShowPrefs();
            //Trigger click event of Save Button  
            var button = FindViewById<Android.Support.Design.Widget.FloatingActionButton>(Resource.Id.btnSavePrefs);
            button.Click += DoSave;
        }

        private void ShowPrefs()
        {
            GetEdit(Resource.Id.txtServerAddress).Text= ServerAuthorization.ServerAddress;
            GetEdit(Resource.Id.txtServerPort).Text=ServerAuthorization.ServerPort;
            GetEdit(Resource.Id.txtDataBaseUserID).Text = ServerAuthorization.UserId;
            GetEdit(Resource.Id.txtDataBasePwd).Text = ServerAuthorization.DataBasePwd;
            GetEdit(Resource.Id.txtDataBaseName).Text= ServerAuthorization.DataBaseName;
        }

        private EditText GetEdit(int res)
        {
           return FindViewById<EditText>(res);
        }

        private void DoSave(object sender, EventArgs e)
        {
            ServerAuthorization.ServerAddress = GetEdit(Resource.Id.txtServerAddress).Text;
            ServerAuthorization.ServerPort = GetEdit(Resource.Id.txtServerPort).Text;
            ServerAuthorization.UserId = GetEdit(Resource.Id.txtDataBaseUserID).Text;
            ServerAuthorization.DataBasePwd = GetEdit(Resource.Id.txtDataBasePwd).Text;
            ServerAuthorization.DataBaseName = GetEdit(Resource.Id.txtDataBaseName).Text;
            StartActivity(typeof(LoginActivity));
        }
    }
}