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

namespace SoilCareAndroid.Activities
{
    [Activity(Label = "EditSolutionActivity", MainLauncher = false)]
    public class EditSolutionActivity : Activity
    {
        ListView listView;
        ImageButton backButton;
        ImageButton saveButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditSolution);


            backButton = FindViewById<ImageButton>(Resource.Id.button_back);
            saveButton = FindViewById<ImageButton>(Resource.Id.button_save);
            listView = FindViewById<ListView>(Resource.Id.list);

            // Get Data using APIConnection
            List<SolutionModel> listSolution = new List<SolutionModel>();
            APIConnection connector = new APIConnection();
            listSolution = connector.GetData<List<SolutionModel>>(APIConnection.Solutions);

            listView.Adapter = new EditSolutionAdapter(this, listSolution);

            backButton.Click += delegate
            {
                this.OnBackPressed();
            };
            saveButton.Click += delegate
            {
                this.OnBackPressed();
                this.Finish();
            };
        }
    }
}