using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System;
using SoilCareAndroid.Fragments;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.View;
using SoilCareAndroid.AdapterClass;
using Android.Views;
using Android.Support.V4.App;
using SoilCareAndroid.ModelClass;
using System.Collections.Generic;
using Android.Content;
using Android.Runtime;
using SoilCareAndroid.Connection;

namespace SoilCareAndroid
{

    [Activity(Label = "@string/app_name", MainLauncher = false)]
    public class MainActivity : FragmentActivity
    {
        BottomNavigationView bottomNavigation;
        public static ViewPager viewPager;
        RootFragment rootFragment;
        HomeFragment homeFragment;
        LibraryFragment libraryFragment;
        AccountFragment accountFragment;
        AddNewLandFragment userLandFragment;

        private checkTelephone check;

        //SettingsFragment settingsFragment;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigation.SetShiftMode(false, false);
            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            viewPager = FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.PageSelected += ViewPager_PageSelected;
            SetUpViewPager(viewPager);
            //sendData();
            
        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            if(e.Position < 2) {
                var item = bottomNavigation.Menu.GetItem(e.Position);
                bottomNavigation.SelectedItemId = item.ItemId;
            }            
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            viewPager.SetCurrentItem(e.Item.Order, true);
        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);
            rootFragment = new RootFragment();
            homeFragment = new HomeFragment();
            libraryFragment = new LibraryFragment();
            accountFragment = new AccountFragment();
            userLandFragment = new AddNewLandFragment();

            // settingsFragment = new SettingsFragment();

            adapter.AddFragment(homeFragment); //0
            adapter.AddFragment(libraryFragment);//1
            adapter.AddFragment(accountFragment);//2
            adapter.AddFragment(userLandFragment);//3
            adapter.AddFragment(new EditUserLandFragment());//4
            adapter.AddFragment(new UserLandFragment());//5

            viewPager.Adapter = adapter;
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        public string getMyData()
        {
            return Intent.GetStringExtra("user_id");
        }
        public override void OnBackPressed()
        {
            if (SupportFragmentManager.BackStackEntryCount > 0)
            {
                SupportFragmentManager.PopBackStack();
            }
            else
            {
                base.OnBackPressed();
            }
        }
    }

}

