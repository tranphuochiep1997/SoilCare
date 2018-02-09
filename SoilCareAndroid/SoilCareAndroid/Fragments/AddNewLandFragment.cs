using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.IO;
using SoilCareAndroid.Connection;
using SoilCareAndroid.ModelClass;
using Java.IO;

namespace SoilCareAndroid.Fragments
{
    public class AddNewLandFragment : global::Android.Support.V4.App.Fragment
    {
        ImageButton imageButton;
        EditText editTextName;
        EditText editTextDes;
        Button btSave;
        Button btCancel;
        ImageButton leftArrow;
        string userId = "";
        private static readonly int CAPTURE_IMAGE_ACTIVITY_REQUEST_CODE = 123;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddNewLandFragment, container, false);
            if(Arguments != null)
            {
                userId = Arguments.GetString("User ID");
            }
            return view;            
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();

            editTextName.RequestFocus();
            leftArrow.Click += LeftArrow_Click;
            btSave.Click += BtSave_Click;
            btCancel.Click += BtCancel_Click;
            imageButton.Click += ImageButton_Click;
        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
                    Bitmap bitmap = (Bitmap)data.Extras.Get("data");
                    imageButton.SetImageBitmap(bitmap);              
        }

        private void ImageButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Activity, "Hello", ToastLength.Short).Show();
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            Activity.StartActivityForResult(intent, 0);
        }

        private void BtCancel_Click(object sender, EventArgs e)
        {           
            Toast.MakeText(this.Activity, "Button Cancel Clicked", ToastLength.Short).Show();
        }

        private void BtSave_Click(object sender, EventArgs e)
        {
            List<LandModel> list = new List<LandModel>();
            APIConnection connector = new APIConnection();
            String name = editTextName.Text;
            String des = editTextDes.Text;
            //
            if (!name.Equals(""))
            {
                bool isNew = connector.PostData(APIConnection.AddNewLand,
                new AddLandModel()
                {
                    Land_name = name,
                    Land_address = des,
                    Land_image = "https://static.cfmobi.vn/upload/news/20170421/crossfire-legends:-che-do-zombie-trong-casual-1492763662486.jpg",
                    User_id = userId,
                    Land_area = 30.0
                });
                if (isNew == true) Toast.MakeText(this.Activity, "Successfully Added", ToastLength.Short).Show();
            }
            else
            {             
                AlertDialog.Builder alert = new AlertDialog.Builder(Context);
                alert.SetTitle("Warning FBI !!!");
                alert.SetMessage("Are you 18+ years old! - Land Name could not be empty");
                alert.SetNegativeButton("Tớ hiểu rồi, Thanks", delegate { alert.Dispose(); });
                Dialog dialog = alert.Create();
                dialog.SetCanceledOnTouchOutside(true);
                dialog.Show();
            }
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.root_frame, new HomeFragment());
            transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            transaction.AddToBackStack(null);
            transaction.Commit();
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