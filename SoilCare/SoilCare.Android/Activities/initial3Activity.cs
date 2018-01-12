using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SoilCare.Android
{
    [Activity(Label = "initial3Activity", ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class initial3Activity : Activity
    {
        private EditText name, location, email;
        private Button later, ok;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.initial3);
            name = FindViewById<EditText>(Resource.Id.fullname);
            location = FindViewById<EditText>(Resource.Id.location);
            email = FindViewById<EditText>(Resource.Id.email);
            later = FindViewById<Button>(Resource.Id.later);
            ok = FindViewById<Button>(Resource.Id.ok);

            ok.Click += Ok_Click;
            later.Click += Later_Click;
            // Create your application here
        }
        private void clearFocus()
        {
            name.ClearFocus();
            email.ClearFocus();
            location.ClearFocus();
        }

        private void Later_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            clearFocus();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            clearFocus();
        }
    }
}