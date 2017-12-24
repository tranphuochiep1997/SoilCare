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
using SoilCare.Android.ModelClass;

using SoilCare.Android.AdapterClass;
using Refractored.Fab;

namespace SoilCare.Android.Fragments
{
    public class HomeFragment : Fragment
    {
        private ListView listView;
        private List<UserLand> list;

        private FloatingActionButton fab;
        
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.HomeFragment, container, false);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            TestData();

            fab.Click += Fab_Click;

        }

        private void Fab_Click(object sender, EventArgs e)
        {
            ShowCustomAlertDialog();
        }
        private void ShowCustomAlertDialog()
        {
            FragmentTransaction transaction = FragmentManager.BeginTransaction();
            NewLandFragment newLand = new NewLandFragment();
            newLand.Show(transaction, "dialog fragment");
        }

        // just testing the data 
        private void TestData()
        {
            list = new List<UserLand>();
            for(int i = 1; i <=5; i++)
            {
                list.Add(new UserLand("User Land "+i, "User Land Description "+i, Resource.Drawable.icon_profilepicture));
            }

            listView.Adapter = new UserLandAdapter(this.Activity, list);           
            
        }
        private void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewLandList);
            fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab1);
            fab.AttachToListView(listView);        
        }            
    }
}