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
using SoilCareAndroid.AdapterClass;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "AddSolutionActivity", MainLauncher = false)]
    public class AddSolutionsActivity : Activity
    {
        ImageButton backButton;
        ImageButton saveButton;
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.AddSolution);

            backButton = FindViewById<ImageButton>(Resource.Id.button_back);
            saveButton = FindViewById<ImageButton>(Resource.Id.button_save);

            backButton.Click += delegate
            {
                this.OnBackPressed();
            };
        }
    }
}