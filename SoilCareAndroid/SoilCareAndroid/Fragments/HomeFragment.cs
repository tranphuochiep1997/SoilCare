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
        LandAdapter adapter;

        private TextView textView;
        //LandAdapter adapter;
        Bundle args = new Bundle();
        string userId = "";

        ISharedPreferences sharedPreferences;
        ISharedPreferencesEditor editor;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HomeFragment, container, false);
            MainActivity main = (MainActivity)this.Activity;
            userId = main.getMyData();

            sharedPreferences =
                Application.Context.GetSharedPreferences("USER_ID", FileCreationMode.Private);
            editor = sharedPreferences.Edit();
            editor.PutString("USER_ID", userId);
            editor.Commit();

            return view;
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            FindViews();
            TestData();
            listView.ItemClick += ListView_ItemClick;
            listView.ItemLongClick += ListView_ItemLongClick;
            buttonAdd.Click += ButtonAdd_Click;
            args.PutString("User ID", userId);           
        }

        private void ListView_ItemLongClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            EditUserLandFragment editUserLandFragment = new EditUserLandFragment();
            var item = this.list[e.Position];
            int index = e.Position;
            editor.PutInt("Index", index);
            editor.Commit();
            MainActivity.viewPager.SetCurrentItem(4, true);

        }
       
        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            MainActivity.viewPager.SetCurrentItem(3, true);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            // REPLACE FRAGMENT HERE
            var item = this.list[e.Position];
            UserLandFragment userLandFragment = new UserLandFragment();
            editor.PutString("LAND NAME", item.Land_name);
            editor.PutString("IMAGE", item.Land_image);
            editor.Commit();
            MainActivity.viewPager.SetCurrentItem(5, true);

        }
        // Get 
        private void TestData()
        {
            // Get Data using APIConnection

            list = new List<LandModel>();
            APIConnection connector = new APIConnection();
            list = connector.GetData<List<LandModel>>(APIConnection.LandsByUserId, userId);

            adapter = new LandAdapter(list, this.Activity);
            //Activity.RunOnUiThread(() => { adapter.refreshEvents(list); });
            listView.Adapter = adapter;
            adapter.NotifyDataSetChanged();
            GetListViewSize(listView);

            if (list == null)
            {
                listView.Visibility = ViewStates.Gone;
                textView.Visibility = ViewStates.Visible;
            }
            else
            {
                listView.Visibility = ViewStates.Visible;
                textView.Visibility = ViewStates.Gone;              
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