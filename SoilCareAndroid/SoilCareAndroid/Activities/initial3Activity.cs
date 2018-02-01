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
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "initial3Activity", ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class initial3Activity : Activity
    {
        private EditText name, location, email;
        private Button later, ok;
        private UserInfo userInfo;
        private APIConnection connector = new APIConnection();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            string user_id = Intent.GetStringExtra("user_id");
            userInfo = connector.GetData<UserInfo>(APIConnection.UserById, user_id);
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
            Intent mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("user_id", userInfo.User_id);
            StartActivity(mainActivity);
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            userInfo.User_email = email.Text;
            userInfo.User_name = name.Text;
            userInfo.User_location = location.Text;
            userInfo.Created_at = DateTime.Now;
            connector.PutData(APIConnection.UserById, userInfo.User_id, userInfo);
            Intent mainActivity = new Intent(this, typeof(MainActivity));
            mainActivity.PutExtra("user_id", userInfo.User_id);
            StartActivity(mainActivity);
        }
    }
}