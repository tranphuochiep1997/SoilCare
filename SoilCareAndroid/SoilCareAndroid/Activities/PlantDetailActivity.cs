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
    [Activity(Label = "PlantDetailActivity")]
    public class PlantDetailActivity : Activity
    {
        private ListView listView;
        private List<UserLand> list;

        ImageButton imageButtonLeftArrow;
        TextView textViewName;
        EditText editText;
        ImageButton ImageButtonSearch;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.PlantDetail);
            FindViews();
            TestData();

            imageButtonLeftArrow.Click += ImageButtonLeftArrow_Click;
            ImageButtonSearch.Click += ImageButtonSearch_Click;
            listView.ItemClick += ListView_ItemClick;
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var userlandHasPlant = new Intent(this, typeof(UserLandHasPlantActivity));
            userlandHasPlant.PutExtra("UserLandHasPlantData", "Data from PlantDetailActivity");
            StartActivity(userlandHasPlant);
        }

        private void ImageButtonSearch_Click(object sender, EventArgs e)
        {
            if(textViewName.Visibility == ViewStates.Visible)
            {
                editText.Visibility = ViewStates.Visible;
                textViewName.Visibility = ViewStates.Gone;
            }
            else
            {
                editText.Visibility = ViewStates.Gone;
                textViewName.Visibility = ViewStates.Visible;
            }
        }

        private void ImageButtonLeftArrow_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void TestData()
        {
            list = new List<UserLand>();
            for (int i = 1; i <= 5; i++)
            {
                list.Add(new UserLand("User Land " + i, "User Land Description " + i, Resource.Drawable.icon_profilepicture));
            }
            listView.Adapter = new UserLandAdapter(this, list);
            
        }

        private void FindViews()
        {
            listView = FindViewById<ListView>(Resource.Id.listViewPlantDetail);
            imageButtonLeftArrow = FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            textViewName = FindViewById<TextView>(Resource.Id.textViewSpecifiedPlant);
            editText = FindViewById<EditText>(Resource.Id.editTextSearchUserLand);
            ImageButtonSearch = FindViewById<ImageButton>(Resource.Id.imageButtonSearchUserLand);
        }
    }
}