using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool switcher = false;
        private APIConnection connector = new APIConnection();
        private GetCode code;
        private checkTelephone check;
        private string phone;
        private string confirm;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.initial2);
            getcode = FindViewById<Button>(Resource.Id.getcode);
            getphone = FindViewById<EditText>(Resource.Id.getphone);
            getcode.Click += Getcode_Click;

        }
        private void alertMes(string title, string mes, string button)
        {
            AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(mes);
            alert.SetButton(button, (c, ev) =>
            {

            });
            alert.Show();
        }
        private void changeType()
        {
            phone = getphone.Text;
            switcher = true;
            getphone.Text = "";
            getphone.Hint = "Enter confirm code";
            getphone.SetFilters(new Android.Text.IInputFilter[] { new Android.Text.InputFilterLengthFilter(6) });
            getcode.Text = "CONFIRM";
            alertMes("Notification", "The code is sending to you as a SMS. Enter your code within 5 minutes!", "OK");
        }
        private bool checkValidPhoneNumber(string s)
        {
            Regex isValidInput = new Regex(@"^\d{9,11}$");
            if (!isValidInput.IsMatch(s))
            {
                alertMes("Sorry", "Your input is not a valid phone number. Try again!", "OK");
                return false;
            }
            return true;
        }
        private void Getcode_Click(object sender, EventArgs e)
        {
            if (!switcher)  //for GETCODE button
            {
                if (checkValidPhoneNumber(getphone.Text))
                {
                    changeType(); //change state of the BUTTON
                    code = connector.GetData<GetCode>(APIConnection.CodeByTelephone, phone);
                }
            }
            else //for CONFIRM button
            {
                int compare = code.Expiration_time.CompareTo(DateTime.Now); //get TIME NOW
                if (compare >= 0) //input confirm code ontime
                {
                    confirm = getphone.Text;
                    if (confirm == code.Verified_code)//correct 
                    {
                        check = connector.GetData<checkTelephone>(APIConnection.UserByTelephone, phone);
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
                        alertMes("Sorry", "You have input an incorrect code. Try again!", "OK");
                }
                else
                {
                    alertMes("Sorry", "The code is expired. You will received another one!", "OK");
                    code = connector.GetData<GetCode>(APIConnection.CodeByTelephone, getphone.Text);
                }
            }
        }
    }
}
    
