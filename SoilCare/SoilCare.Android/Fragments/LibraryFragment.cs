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

namespace SoilCare.Android.Fragments
{
    public class LibraryFragment : global::Android.Support.V4.App.Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            HasOptionsMenu = true;
            // Create your fragment here
        }


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.LibraryFragment, container, false);
            Toolbar toolbar = view.FindViewById<Toolbar>(Resource.Id.toolbar);
            Activity.SetActionBar(toolbar);
            toolbar.Title = "LIBRARY";
            return view;
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Menu.search_toolbar, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this.Activity, "Action selected: " + item.TitleFormatted,
                ToastLength.Short).Show();            
            return base.OnOptionsItemSelected(item);
        }


    }
}