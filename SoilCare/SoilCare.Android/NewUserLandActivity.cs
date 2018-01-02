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
using Android.Content.PM;

namespace SoilCare.Android
{
    [Activity(Label = "NewUserLandActivity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class NewUserLandActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewUserLand);
            // Create your application here
        }
    }
}