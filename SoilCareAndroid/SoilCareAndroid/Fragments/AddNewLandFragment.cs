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
using Uri = Android.Net.Uri;
using Java.IO;
using Android.Content.PM;

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
        String link = "";
        private static readonly int REQUEST_IMAGE_CAPTURE = 123;

        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor editor;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);           
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.AddNewLandFragment, container, false);

            sharedPreferences =
                Application.Context.GetSharedPreferences("USER_ID", FileCreationMode.Private);
            userId = sharedPreferences.GetString("USER_ID", null);

            return view;            
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();

            leftArrow.Click += LeftArrow_Click;
            btSave.Click += BtSave_Click;
            btCancel.Click += BtCancel_Click;
            //imageButton.Click += ImageButton_Click;
            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();
                imageButton.Click += TakeAPicture;
            }

        }
        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Intent medianScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            Uri contenUri = Uri.FromFile(App._file);
            medianScanIntent.SetData(contenUri);
            Activity.SendBroadcast(medianScanIntent);
            int height = Resources.DisplayMetrics.HeightPixels;
            int width = imageButton.Height;
            App.bitmap = App._file.Path.LoadAndResizeBitmap(width, height);
            if(App.bitmap != null) {                 
                imageButton.SetImageBitmap(App.bitmap);
                APIConnection connector = new APIConnection();
                link = connector.PostImage(App.bitmap, "land");
                App.bitmap = null;
            }
            GC.Collect();
                                    
        }        

        private void ImageButton_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this.Activity, "Hello", ToastLength.Short).Show();
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            this.StartActivityForResult(intent, REQUEST_IMAGE_CAPTURE);
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
                    Land_image = link,
                    User_id = userId,
                    Land_area = 30.0
                });
                if (isNew == true) Toast.MakeText(this.Activity, "Successfully Added", ToastLength.Short).Show();
            }
            else
            {             
                AlertDialog.Builder alert = new AlertDialog.Builder(Context);
                alert.SetTitle("FBI Warning!!!");
                alert.SetMessage("Are you 18+ years old! - Land Name could not be empty");
                alert.SetNegativeButton("Tớ hiểu rồi, Thanks", delegate { alert.Dispose(); });
                Dialog dialog = alert.Create();
                dialog.SetCanceledOnTouchOutside(true);
                dialog.Show();
            }
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            MainActivity.viewPager.SetCurrentItem(0, true);
        }

        private void FindViews()
        {
            leftArrow = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonLeftArrow);
            imageButton = this.View.FindViewById<ImageButton>(Resource.Id.imageViewNewUserLand);
            editTextName = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandName);
            editTextDes = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandLocation);
            btSave = this.View.FindViewById<Button>(Resource.Id.buttonSave);
            btCancel = this.View.FindViewById<Button>(Resource.Id.buttonCancle);
        }
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            App._file = new Java.IO.File(App._dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));
            intent.PutExtra(MediaStore.ExtraOutput, Uri.FromFile(App._file));
            StartActivityForResult(intent, 0);

            Toast.MakeText(this.Activity, App._file + "", ToastLength.Short).Show();
        }
        private void CreateDirectoryForPictures()
        {
            App._dir = new Java.IO.File(
                Android.OS.Environment.GetExternalStoragePublicDirectory(
                    Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!App._dir.Exists())
            {
                App._dir.Mkdirs();
            }
        }

        private bool IsThereAnAppToTakePictures()
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities =
                Activity.PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }
    }
    public static class App
    {
        public static Java.IO.File _file;
        public static Java.IO.File _dir;
        public static Bitmap bitmap;
    }
}