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
        ImageButton leftArrow;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.NewUserLand);
            FindViews();

            imageButton.Click += ImageButton_Click;
            leftArrow.Click += LeftArrow_Click;

        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            
        }

        private void FindViews()
        {
            leftArrow = FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            imageButton = FindViewById<ImageButton>(Resource.Id.imageButtonNewUserLand);
            editTextName = FindViewById<EditText>(Resource.Id.editTextNewLandName);
            editTextDes = FindViewById<EditText>(Resource.Id.editTextNewLandLocation);
            btSave = FindViewById<Button>(Resource.Id.buttonSave);
            btCancel = FindViewById<Button>(Resource.Id.buttonCancle);
        }
    }
}