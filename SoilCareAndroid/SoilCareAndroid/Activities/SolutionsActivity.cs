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
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "SolutionsActivity", MainLauncher = true)]
    public class SolutionsActivity : Activity
    {
        ListView listView;
        ImageButton backButton;
        ImageButton rateButton;
        ImageButton editButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
 
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Solutions);


            backButton = FindViewById<ImageButton>(Resource.Id.button_back);
            rateButton = FindViewById<ImageButton>(Resource.Id.button_rate);
            editButton = FindViewById<ImageButton>(Resource.Id.button_edit);
            listView = FindViewById<ListView>(Resource.Id.list);

            // Get Data using APIConnection
            List<SolutionModel> listSolution = new List<SolutionModel>();
            APIConnection connector = new APIConnection();
            listSolution = connector.GetData<List<SolutionModel>>(APIConnection.Solutions);
            
            listView.Adapter = new SolutionsAdapter(this, listSolution);

            backButton.Click += delegate
            {
                this.OnBackPressed();
            };
            editButton.Click += delegate
            {
                Intent addSolutionActivity = new Intent(this, typeof(AddSolutionsActivity));
                StartActivity(addSolutionActivity);
            };
        }
        
    }
}