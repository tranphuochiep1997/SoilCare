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
    public class HomeFragment : global::Android.Support.V4.App.Fragment
    {
        private ImageButton buttonAdd;
        private ListView listView;
        private List<UserLand> list;

        //private FloatingActionButton fab;
        
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.HomeFragment, container, false);
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

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            var newuserland = new Intent(this.Activity, typeof(NewUserLandActivity));
            newuserland.PutExtra("NewUserLandData", "Data from HomeActivity");
            StartActivity(newuserland);
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            Toast.MakeText(this.Activity, "List View Item Clicked", ToastLength.Short).Show();

            var userland = new Intent(this.Activity, typeof(UserLandActivity));
            userland.PutExtra("UserLandData", "Data from HomeActivity");
            StartActivity(userland);
        }

        // IT WILL BE DELETED SOON

        //private void Fab_Click(object sender, EventArgs e)
        //{
        //    ShowCustomAlertDialog();
        //}
        //private void ShowCustomAlertDialog()
        //{
        //    FragmentTransaction ft = FragmentManager.BeginTransaction();
        //    //Remove fragment else it will crash as it is already added to backstack
        //    Fragment prev = FragmentManager.FindFragmentByTag("dialog fragment");
        //    if(prev != null)
        //    {
        //        ft.Remove(prev);
        //    }
        //    ft.AddToBackStack(null);

        //    FragmentTransaction transaction = FragmentManager.BeginTransaction();
        //    NewLandFragment newLand = new NewLandFragment();
        //    newLand.Show(transaction, "dialog fragment");
        //}

        // just testing the data 
        private void TestData()
        {
            list = new List<UserLand>();
            for(int i = 1; i <=5; i++)
            {
                list.Add(new UserLand("User Land "+i, "User Land Description "+i, Resource.Drawable.icon_profilepicture));
            }
            listView.Adapter = new UserLandAdapter(this.Activity, list);
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