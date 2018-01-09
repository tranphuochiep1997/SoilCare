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

namespace SoilCare.Android
{
    [Activity(Label = "initial2Activity")]
    public class initial2Activity : Activity
    {
        private Button getcode;
        private EditText getphone;
        private EditText confirm;
        private TextView noti;
        private bool switcher = false;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.initial2);
            getcode = FindViewById<Button>(Resource.Id.getcode);
            getphone = FindViewById<EditText>(Resource.Id.getphone);
            confirm = FindViewById<EditText>(Resource.Id.confirm);
            noti = FindViewById<TextView>(Resource.Id.noti);
            getcode.Click += Getcode_Click;

        }

        private void Getcode_Click(object sender, EventArgs e)
        {
            if (!switcher)
            {
                getcode.Text = "CONFIRM";
                switcher = true;
                getphone.ClearFocus();
                noti.Text = "The code is sending to you as a SMS in several seconds!";
            }
            else
                StartActivity(typeof(initial3Activity));
        }
    }
}