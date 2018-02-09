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
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
             return inflater.Inflate(Resource.Layout.EditUserLandFragment, container, false);
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            leftArrow.Click += LeftArrow_Click;
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