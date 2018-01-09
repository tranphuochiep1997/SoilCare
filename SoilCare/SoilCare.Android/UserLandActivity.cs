using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace SoilCare.Android
{
    [Activity(Label = "UserLandActivity", MainLauncher =false, Theme = "@style/CustomActionBarTheme")]
    public class UserLandActivity : Activity
    {
        ImageButton imageButton;
        TextView textViewName;
        ImageView imageView;
        Button button;
        ListView listView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserLand);
            // Create your application here
            FindViews();
            imageButton.Click += ImageButton_Click;
            button.Click += Button_Click;

        }

        private void Button_Click(object sender, EventArgs e)
        {
            
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MainActivity));
            Finish();
        }

        private void FindViews()
        {
            imageButton = FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            textViewName = FindViewById<TextView>(Resource.Id.textViewSpecifiedLand);
            imageView = FindViewById<ImageView>(Resource.Id.imageViewSpecifiedUserLand);
            button = FindViewById<Button>(Resource.Id.buttonMeasure);
            listView = FindViewById<ListView>(Resource.Id.listViewMeasuredUserLand);
        }
    }
}