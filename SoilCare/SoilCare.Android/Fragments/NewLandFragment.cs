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
using Android.Provider;
using Android.Graphics;
using Java.IO;
using Environment = Android.OS.Environment;
using Uri = Android.Net.Uri;
using Android.Content.PM;
using Android.Graphics.Drawables;

namespace SoilCare.Android.Fragments
{
    public class NewLandFragment: DialogFragment
    {
        private ImageButton ib;
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.NewUserLand,container, false);
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
           // Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            Dialog.Window.Attributes.WindowAnimations = Resource.Style.dialog_animation;
            Dialog.Window.SetBackgroundDrawable(new ColorDrawable(Color.AliceBlue));


            // finding views
            var edtName = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandName);
            var edtDes = this.View.FindViewById<EditText>(Resource.Id.editTextNewLandLocation);
            ib = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonNewUserLand);
            var buttonSave = this.View.FindViewById<Button>(Resource.Id.buttonSave);
            var buttonCancel = this.View.FindViewById<Button>(Resource.Id.buttonCancle);


            buttonSave.Click += delegate
            {
                
                Toast.MakeText(this.Activity, "Saved", ToastLength.Short).Show();
            };
            

            buttonCancel.Click += delegate
            {
                Dismiss();
                Toast.MakeText(this.Activity, "Canceled", ToastLength.Short).Show();
            };

            ib.Click += delegate
            {
                Toast.MakeText(this.Activity, "Camera Clicked !!!!", ToastLength.Short).Show();
                Intent intent = new Intent(MediaStore.ActionImageCapture);
                StartActivityForResult(intent, 0);
            };
            
        }

        public override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            Bitmap bitmap = (Bitmap)data.Extras.Get("data");
           
            ib.SetImageBitmap(bitmap);
        }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            Dialog dialog = base.OnCreateDialog(savedInstanceState);
            dialog.SetTitle(" Add Your New Land");        
            return dialog;
        }


    }
}