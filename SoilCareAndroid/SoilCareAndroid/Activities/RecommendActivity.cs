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

namespace SoilCareAndroid
{
    [Activity(Label = "RecommendActivity")]
    public class RecommendActivity : Activity
    {
        ImageButton imageButton;
        ListView listView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Recommend);

            FindViews();
            imageButton.Click += ImageButton_Click;
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(MeasureActivity));
            Finish();
        }

        private void FindViews()
        {
            imageButton = FindViewById<ImageButton>(Resource.Id.imageButtonClose);
            listView = FindViewById<ListView>(Resource.Id.listViewRecommend);
        }
    }
}