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
using Android.Content.PM;
using Android.Support.V7.App;

namespace SoilCare.Android
{
    [Activity(Label = "Add Your New Land", ScreenOrientation = ScreenOrientation.Portrait, Theme = "@style/CustomActionBarTheme")]
    public class NewUserLandActivity : Activity
    {
        ImageButton imageButton;
        EditText editTextName;
        EditText editTextDes;
        Button btSave;
        Button btCancel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewUserLand);

            Toolbar toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            toolbar.Title = "ADD YOUR NEW LAND HERE";
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            FindViews();


        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case global::Android.Resource.Id.Home:
                    Finish();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
           
        }
        private void FindViews()
        {
            imageButton = FindViewById<ImageButton>(Resource.Id.imageButtonNewUserLand);
            editTextName = FindViewById<EditText>(Resource.Id.editTextNewLandName);
            editTextDes = FindViewById<EditText>(Resource.Id.editTextNewLandLocation);
            btSave = FindViewById<Button>(Resource.Id.buttonSave);
            btCancel = FindViewById<Button>(Resource.Id.buttonCancle);
        }
    }
}