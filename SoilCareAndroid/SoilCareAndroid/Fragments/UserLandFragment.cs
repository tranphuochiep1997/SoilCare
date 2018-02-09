using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Util;
using Android.Views;
using Android.Widget;
using SoilCareAndroid.AdapterClass;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.Fragments
{
    public class UserLandFragment : global::Android.Support.V4.App.Fragment
    {
        ImageButton imageButton;
        TextView textViewName;
        ImageView imageView;
        Button button;
        ListView listView;
        private List<PlantInfo> list;
        private LibraryAdapter adapter;
        string landName = "";
        string imagePath = "";

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View result = inflater.Inflate(Resource.Layout.UserLandFragment, container, false);
            if (Arguments != null)
            {
                landName = Arguments.GetString("Land Name");
                imagePath = Arguments.GetString("Image Path");
            }
            return result;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);

            FindViews();
            //
            textViewName.Text = landName;
            var bitmapImage = BitmapHelper.GetImageBitmapFromUrl(imagePath);
            imageView.SetImageBitmap(bitmapImage);

            TestData();
            imageButton.Click += ImageButton_Click;
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            var measureActivity = new Intent(this.Activity, typeof(MeasureActivity));
            measureActivity.PutExtra("MeasureActivityData", "Data from UserLandActivity");
            StartActivity(measureActivity);
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.root_frame, new HomeFragment());
            transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        private void FindViews()
        {
            imageButton = View.FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            textViewName = View.FindViewById<TextView>(Resource.Id.textViewSpecifiedLand);
            imageView = View.FindViewById<ImageView>(Resource.Id.imageViewSpecifiedUserLand);
            button = View.FindViewById<Button>(Resource.Id.buttonMeasure);
            listView = View.FindViewById<ListView>(Resource.Id.listViewMeasuredUserLand);
        }

        private void TestData()
        {
            list = new List<PlantInfo>();

            // Item click events showed wrong results , fix later.
            list.Add(new PlantInfo("Cam", " Trung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Tao", " Lao ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Buoi", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Oi", " Trung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Xoai", " Thai Lan ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Man", " Nga ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Dao", " My ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Nho", " Quang Binh ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Quyt", " Trung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Le", " Viet Nam ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mang Cau", " TRung Quoc ", Resource.Drawable.icon_soilcare2));
            list.Add(new PlantInfo("Mit uot", " Han Quoc ", Resource.Drawable.icon_soilcare2));

            adapter = new LibraryAdapter(this.Activity, list);
            listView.Adapter = adapter;
            listView.TextFilterEnabled = true;
        }
    }
}