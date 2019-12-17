using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;


namespace AttendanceJournal
{
    class ServerAuthorization
    {
        public static string ServerAddress 
        {
            get => GetPrefs(NamesOfNodes.serverAddress);
            set => SetPrefs(NamesOfNodes.serverAddress, value);
        }

        public static string ServerPort 
        {
            get => GetPrefs(NamesOfNodes.serverPort);
            set => SetPrefs(NamesOfNodes.serverPort, value);
        }
        public static string UserId
        {
            get => GetPrefs(NamesOfNodes.DataBaseUserID);
            set => SetPrefs(NamesOfNodes.DataBaseUserID, value);
        }
        public static string DataBasePwd
        {
            get => GetPrefs(NamesOfNodes.DataBasePwd);
            set => SetPrefs(NamesOfNodes.DataBasePwd, value);
        }
        public static string DataBaseName
        {
            get => GetPrefs(NamesOfNodes.DataBaseName);
            set => SetPrefs(NamesOfNodes.DataBaseName, value);
        }

        enum NamesOfNodes
        {
            serverAddress,
            serverPort,
            DataBaseUserID,
            DataBasePwd,
            DataBaseName
        }

        static string GetNameOfNode(NamesOfNodes name)
        {
            return Enum.GetName(typeof(NamesOfNodes), name);
        }

        private static ISharedPreferences Prefs
        {
            get => Application.Context.GetSharedPreferences(Application.Context.PackageName, FileCreationMode.Private);
        }

        static string GetPrefs(NamesOfNodes name)
        {
            return Prefs.GetString(GetNameOfNode(name), null);
        }

        static void SetPrefs(NamesOfNodes name,string @value)
        {
            var editor = Prefs.Edit();
            editor.PutString(GetNameOfNode(name), value);
            editor.Commit();
        }
    }
}