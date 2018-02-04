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
using SoilCareAndroid.ModelClass;
using SoilCareAndroid.AdapterClass;
using Refractored.Fab;
using SoilCareAndroid.Connection;

namespace SoilCareAndroid.Fragments
{
    public class HomeFragment : global::Android.Support.V4.App.Fragment
    {
        private ImageButton buttonAdd;
        private ListView listView;
        private List<LandModel> list;

        //private FloatingActionButton fab;
        string userId = "";
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HomeFragment, container, false);
            MainActivity main = (MainActivity)this.Activity;
            userId = main.getMyData();
            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            
            TestData();
            listView.ItemClick += ListView_ItemClick;
            buttonAdd.Click += ButtonAdd_Click;

            //fab.Click += Fab_Click;

        }
        private void ReplaceFragment()
        {
            global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.root_frame, new UserLandFragment());
            transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var newuserland = new Intent(this.Activity, typeof(NewUserLandActivity));
            newuserland.PutExtra("NewUserLandData", "Data from HomeActivity");
            StartActivity(newuserland);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // REPLACE FRAGMENT HERE
            var item = this.list[e.Position];
            UserLandFragment userLandFragment = new UserLandFragment();
            Bundle args = new Bundle();
            args.PutString("Land Name",item.Land_name);
            args.PutString("Image Path", item.Land_image);
            userLandFragment.Arguments = args;
            


            global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.root_frame, userLandFragment);
            transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }

        // Get 
        private void TestData()
        {
            // Get Data using APIConnection
            list = new List<LandModel>();
            APIConnection connector = new APIConnection();
            //list = connector.GetData<List<LandModel>>(APIConnection.LandsByUserId, userId);
            list = connector.GetData<List<LandModel>>(APIConnection.UserLand);
            listView.Adapter = new LandAdapter(list, this.Activity);

            GetListViewSize(listView);
        }

        private void GetListViewSize(ListView myListView)
        {
            IListAdapter myListAdapter = myListView.Adapter;
            if (myListAdapter == null)
            {
                //do nothing return null
                return;
            }
            //set listAdapter in loop for getting final size
            int totalHeight = 0;
            for (int size = 0; size < myListAdapter.Count; size++)
            {
                View listItem = myListAdapter.GetView(size, null, myListView);
                listItem.Measure(0, 0);
                totalHeight += listItem.MeasuredHeight;
            }
            //setting listview item in adapter
            //ViewGroup.LayoutParams params = myListView.getLayoutParams();
            ViewGroup.LayoutParams params2 = myListView.LayoutParameters;
            params2.Height = totalHeight + (myListView.DividerHeight * (myListAdapter.Count) - 1);
            myListView.LayoutParameters = params2;            
        }

        private void FindViews()
        {
            listView = this.View.FindViewById<ListView>(Resource.Id.listViewLandList);
            buttonAdd = this.View.FindViewById<ImageButton>(Resource.Id.imageButtonAdd);
            //fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab1);
            //fab.AttachToListView(listView);        
        }

   
    }
}