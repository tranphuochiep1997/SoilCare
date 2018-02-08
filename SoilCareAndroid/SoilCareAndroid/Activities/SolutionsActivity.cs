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
using SoilCareAndroid.Activities;
using SoilCareAndroid.AdapterClass;
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid
{
    [Activity(Label = "SolutionsActivity", MainLauncher = false)]
    public class SolutionsActivity : Activity
    {
        ListView listView;
        ImageButton backButton;
        ImageButton rateButton;
        ImageButton editButton;
        ImageView addButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
 
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Solutions);


            LayoutInflater inflater = Window.LayoutInflater;
            View footerView = ((LayoutInflater)inflater.Context.GetSystemService(Context.LayoutInflaterService))
                .Inflate(Resource.Layout.FooterView, null, false);


            backButton = FindViewById<ImageButton>(Resource.Id.button_back);
            rateButton = FindViewById<ImageButton>(Resource.Id.button_rate);
            editButton = FindViewById<ImageButton>(Resource.Id.button_edit);
            listView = FindViewById<ListView>(Resource.Id.list);
            listView.AddFooterView(footerView);

            addButton = FindViewById<ImageView>(Resource.Id.add_button);

            // Get Data using APIConnection
            List <SolutionModel> listSolution = new List<SolutionModel>();
            APIConnection connector = new APIConnection();
            listSolution = connector.GetData<List<SolutionModel>>(APIConnection.Solutions);
            
            listView.Adapter = new SolutionsAdapter(this, listSolution);

            addButton.Click += delegate
            {
                StartActivity(typeof(AddSolutionsActivity));
            };
            backButton.Click += delegate
            {
                this.OnBackPressed();
            };
            editButton.Click += delegate
            {
                Intent editSolutionActivity = new Intent(this, typeof(EditSolutionActivity));
                StartActivity(editSolutionActivity);
            };
        }
        
    }
}