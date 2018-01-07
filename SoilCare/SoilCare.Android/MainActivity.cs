using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System;
using SoilCare.Android.Fragments;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Support.V4.View;
using SoilCare.Android.AdapterClass;
using Android.Views;
using Android.Support.V4.App;

namespace SoilCare.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/CustomActionBarTheme")]
    public class MainActivity : FragmentActivity
    {
        BottomNavigationView bottomNavigation;
        ViewPager viewPager;

        HomeFragment homeFragment;
        LibraryFragment libraryFragment;
        AccountFragment accountFragment;
        SettingsFragment settingsFragment;
<<<<<<< HEAD

=======
>>>>>>> 5485ab2031398304ed353a2ef9aaa7ad81ba0a18

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

        }

        private void ViewPager_PageSelected(object sender, ViewPager.PageSelectedEventArgs e)
        {
            var item = bottomNavigation.Menu.GetItem(e.Position);
            bottomNavigation.SelectedItemId = item.ItemId;
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            viewPager.SetCurrentItem(e.Item.Order, true);

        }

        private void SetUpViewPager(ViewPager viewPager)
        {
            ViewPagerAdapter adapter = new ViewPagerAdapter(SupportFragmentManager);
            homeFragment = new HomeFragment();
            libraryFragment = new LibraryFragment();
            accountFragment = new AccountFragment();
            settingsFragment = new SettingsFragment();

            adapter.AddFragment(homeFragment);
            adapter.AddFragment(libraryFragment);
            adapter.AddFragment(accountFragment);
            adapter.AddFragment(settingsFragment);
            viewPager.Adapter = adapter;
        }



    }
}

