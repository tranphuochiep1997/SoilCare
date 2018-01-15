using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoilCareAndroid
{
    [Activity(Label = "SoilCare", MainLauncher=true, ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class initial1Activity : Activity
    {
        System.Timers.Timer timer = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.initial1);

            timer = new System.Timers.Timer();
            timer.Interval = 3000;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(t_Elapsed);
            timer.Start();
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            StartActivity(typeof(initial2Activity));
            Finish();
        }

    }
}
