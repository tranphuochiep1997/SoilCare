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
using SoilCareAndroid.Fragments;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "initial2Activity", ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class initial2Activity : Activity
    {
        private Button getcode;
        private EditText getphone;
        private EditText confirm;
        private TextView noti;
        private bool switcher = false;
        private APIConnection connector = new APIConnection();
        private GetCode code;
        private checkTelephone check;
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
                code = connector.GetData<GetCode>(APIConnection.CodeByTelephone, getphone.Text);
                getphone.Focusable = false;
            }
            else
            {
                int compare = code.Expiration_time.CompareTo(DateTime.Now);
                if (compare >= 0) //input confirm code ontime
                    if (confirm.Text==code.Verified_code)
                    {
                        check = connector.GetData<checkTelephone>(APIConnection.UserByTelephone, getphone.Text);
                        if (check.IsNew) //newbie
                        {
                            Intent initial3 = new Intent(this, typeof(initial3Activity));
                            initial3.PutExtra("user_id", check.User_id);
                            StartActivity(initial3);
                        }
                        else //pro
                        {
                            //Bundle bunlde = new Bundle();
                            //bunlde.PutString("user_id", check.User_id);
                            //HomeFragment home = new HomeFragment();
                            //home.Arguments = bunlde;

                            Intent mainActivity = new Intent(this, typeof(MainActivity));
                            mainActivity.PutExtra("user_id", check.User_id);
                            StartActivity(mainActivity);
                        }
                    }
                    else
                    {
                        noti.Text = "You have input an incorrect code. Try again!";
                    }
                else
                {
                    noti.Text = "The code is expired. You will received another one!";
                    code = connector.GetData<GetCode>(APIConnection.CodeByTelephone, getphone.Text);
                }
            }
        }
    }
}
    
