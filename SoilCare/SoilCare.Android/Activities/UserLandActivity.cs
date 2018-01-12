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
using SoilCare.Android.AdapterClass;
using SoilCare.Android.ModelClass;

namespace SoilCare.Android
{
    [Activity(Label = "UserLandActivity", MainLauncher =false)]
    public class UserLandActivity : Activity
    {
        ImageButton imageButton;
        TextView textViewName;
        ImageView imageView;
        Button button;
        ListView listView;
        private List<PlantInfo> list;
        private LibraryAdapter adapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.UserLand);
            // Create your application here
            FindViews();
            TestData();
            imageButton.Click += ImageButton_Click;
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var measureActivity = new Intent(this, typeof(MeasureActivity));
            measureActivity.PutExtra("MeasureActivityData", "Data from UserLandActivity");
            StartActivity(measureActivity);
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

        private void TestData()
        {
            list = new List<PlantInfo>();

            // Item click events showed wrong results , fix later.
            list.Add(new PlantInfo("Cam", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Tao", " Lao ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Buoi", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Oi", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Xoai", " Thai Lan ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Man", " Nga ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Dao", " My ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Nho", " Quang Binh ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Quyt", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Le", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mang Cau", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mit uot", " Han Quoc ", Resource.Drawable.icon_soilcare2));

            adapter = new LibraryAdapter(this, list);
            listView.Adapter = adapter;
            listView.TextFilterEnabled = true;
        }
    }
}