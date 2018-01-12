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
    [Activity(Label = "MeasureHasPlantActivity")]
    public class MeasureHasPlantActivty : Activity
    {
        ImageButton imageButton;
        TextView specifiedUserLand;
        TextView location;
        TextView humidity;
        TextView acidity;
        TextView nutrient;
        TextView salinity;
        TextView porosity;
        TextView waterRetentention;

        TextView locationLevel;
        TextView humidityLevel;
        TextView acidityLevel;
        TextView nutrientLevel;
        TextView salinityLevel;
        TextView porosityLevel;
        TextView waterRetententionLevel;

        Button recommend;
        Button plants;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.MeasureHasPlant);
            FindViews();

            imageButton.Click += ImageButton_Click;
            recommend.Click += Recommend_Click;
            plants.Click += Plants_Click;
        }

        private void Plants_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "CLICK CC", ToastLength.Short).Show();
        }

        private void Recommend_Click(object sender, EventArgs e)
        {
            var recommendActivity = new Intent(this, typeof(RecommendActivity));
            //newuserland.PutExtra("NewUserLandData", "Data from HomeActivity");
            StartActivity(recommendActivity);
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void FindViews()
        {
            imageButton = FindViewById<ImageButton>(Resource.Id.imageButtonClose);
            specifiedUserLand = FindViewById<TextView>(Resource.Id.textViewSpecifiedLand);
            location = FindViewById<TextView>(Resource.Id.textViewLocation);
            humidity = FindViewById<TextView>(Resource.Id.textViewHumidity);
            acidity = FindViewById<TextView>(Resource.Id.textViewAcidity);
            nutrient = FindViewById<TextView>(Resource.Id.textViewNutrient);
            salinity = FindViewById<TextView>(Resource.Id.textViewSalinity);
            porosity = FindViewById<TextView>(Resource.Id.textViewPorosity);
            waterRetentention = FindViewById<TextView>(Resource.Id.textViewWaterRetention);
            recommend = FindViewById<Button>(Resource.Id.buttonRecommend);
            plants = FindViewById<Button>(Resource.Id.buttonPlants);
        }
    }
}