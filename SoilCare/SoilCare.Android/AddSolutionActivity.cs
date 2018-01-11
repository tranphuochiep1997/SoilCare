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
using SoilCare.Android.AdapterClass;
using SoilCare.Android.ModelClass;

namespace SoilCare.Android
{
    [Activity(Label = "AddSolutionActivity", MainLauncher = false, Theme = "@style/CustomActionBarTheme")]
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