using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "SoilCare", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class initial1Activity : Activity
    {
        System.Timers.Timer timer = null;
        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor editor;
        string user_id;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            sharedPreferences =
                Application.Context.GetSharedPreferences("USER_ID", FileCreationMode.Private);
            editor = sharedPreferences.Edit();
            SetContentView(Resource.Layout.initial1);

            timer = new System.Timers.Timer();
            timer.Interval = 3000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            timer.Start();
        }

        private void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            loadUserID();
            if (user_id == "") //logged out
            {
                StartActivity(typeof(initial2Activity));
            }
            else //load logged in user id
            {
                Intent mainActivity = new Intent(this, typeof(MainActivity));
                mainActivity.PutExtra("user_id", user_id);
                StartActivity(mainActivity);
            }
            Finish();
        }
        private void loadUserID() //load userid from sharedPreferences
        {
            if (sharedPreferences != null)
            {
                user_id = sharedPreferences.GetString("user_id", "");
            }
        }
    }
}
