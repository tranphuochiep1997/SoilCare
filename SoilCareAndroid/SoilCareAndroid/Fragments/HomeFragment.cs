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

        private TextView textView;
        //LandAdapter adapter;

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
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            AddNewLandFragment addNewLandFragment = new AddNewLandFragment();
            Bundle args = new Bundle();
            args.PutString("User ID", userId);
            addNewLandFragment.Arguments = args;
            ReplaceFragment(addNewLandFragment);           
        }
        public void ReplaceFragment(global::Android.Support.V4.App.Fragment fragment)
        {
            //AddNewLandFragment addNewLandFragment = new AddNewLandFragment();

            global::Android.Support.V4.App.FragmentTransaction transaction = FragmentManager.BeginTransaction();
            transaction.Replace(Resource.Id.root_frame, fragment);
            transaction.SetTransition(global::Android.Support.V4.App.FragmentTransaction.TransitFragmentOpen);
            transaction.AddToBackStack(null);
            transaction.Commit();
        }
        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // REPLACE FRAGMENT HERE
            var item = this.list[e.Position];
            UserLandFragment userLandFragment = new UserLandFragment();
            Bundle args = new Bundle();
            args.PutString("Land Name", item.Land_name);
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
            list = connector.GetData<List<LandModel>>(APIConnection.LandsByUserId, userId);
            if(list == null)
            {
                listView.Visibility = ViewStates.Gone;
                textView.Visibility = ViewStates.Visible;
            }
            else {
                listView.Visibility = ViewStates.Visible;
                textView.Visibility = ViewStates.Gone;
                listView.Adapter = new LandAdapter(list, this.Activity);
                GetListViewSize(listView);
            }           
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
            textView = this.View.FindViewById<TextView>(Resource.Id.textViewAlert);
            //fab = this.View.FindViewById<FloatingActionButton>(Resource.Id.fab1);
            //fab.AttachToListView(listView);        
        }


    }
}