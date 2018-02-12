using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;

namespace SoilCareAndroid.Fragments
{
    public class EditUserLandFragment : global::Android.Support.V4.App.Fragment
    {
        ImageButton imageButton;
        EditText editTextName;
        EditText editTextDes;
        Button btSave;
        Button btCancel;
        ImageButton leftArrow;
        String userId = "";
        int index = -1;
        APIConnection connector = new APIConnection();
        List<LandModel> list;
        ISharedPreferences sharedPreferences;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            View view = inflater.Inflate(Resource.Layout.EditUserLandFragment, container, false);

            sharedPreferences =
                Application.Context.GetSharedPreferences("USER_ID", FileCreationMode.Private);
            userId = sharedPreferences.GetString("USER_ID", null);
            index = sharedPreferences.GetInt("Index", -1);

            return view;
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            leftArrow.Click += LeftArrow_Click;
            btSave.Click += BtSave_Click;
            btCancel.Click += BtCancel_Click;

            if(userId != null)
            {               
                if(index >=0)
                {
                    list = connector.GetData<List<LandModel>>(APIConnection.LandsByUserId, userId);
                    var item = list[index];
                    var bitmap = BitmapHelper.GetImageBitmapFromUrl(item.Land_image);
                    imageButton.SetImageBitmap(bitmap);
                    editTextName.Text = item.Land_name;
                    editTextDes.Text = item.Land_address;
                } 
            }
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {
            MainActivity.viewPager.SetCurrentItem(3, true);
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            MainActivity.viewPager.SetCurrentItem(0, true);
        }

        private void FindViews()
        {
            leftArrow = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            imageButton = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonNewUserLand);
            editTextName = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandName);
            editTextDes = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandLocation);
            btSave = this.View.FindViewById<Button>(Resource.Id.buttonSave);
            btCancel = this.View.FindViewById<Button>(Resource.Id.buttonCancle);
        }
    }
}