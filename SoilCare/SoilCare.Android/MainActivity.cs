using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Support.Design.Widget;
using System;
using SoilCare.Android.Fragments;

namespace SoilCare.Android
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        BottomNavigationView bottomNavigation;
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            //var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            //if (toolbar != null)
            //{
            //    SetSupportActionBar(toolbar);
            //    SupportActionBar.SetDisplayHomeAsUpEnabled(false);
            //    SupportActionBar.SetHomeButtonEnabled(false);

            //}

            bottomNavigation = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);

            bottomNavigation.SetShiftMode(false, false);

            bottomNavigation.NavigationItemSelected += BottomNavigation_NavigationItemSelected;

            LoadFragment(Resource.Id.menu_home);
            
        }

        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        private void LoadFragment(int id)
        {
            Fragment fragment = null;   
            switch (id)
            {
                case Resource.Id.menu_home:
                    fragment = new HomeFragment();
                    ReplaceFragment(fragment);
                    break;
                case Resource.Id.menu_library:
                    fragment = new LibraryFragment();
                    ReplaceFragment(fragment);
                    break;
                case Resource.Id.menu_account:
                    fragment = new AccountFragment();
                    ReplaceFragment(fragment);
                    break;
                case Resource.Id.menu_settings:
                    fragment = new SettingsFragment();
                    ReplaceFragment(fragment);
                    break;
            }
            if (fragment == null)
                return;

          

        }

        private void ReplaceFragment(Fragment fragment)
        {
            // Create a new fragment and a transaction.
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();

            // Replace the fragment that is in the View fragment_container (if applicable).
            fragmentTx.Replace(Resource.Id.content_frame, fragment);

            // Add the transaction to the back stack.
            fragmentTx.AddToBackStack(null);

            // Commit the transaction.
            fragmentTx.Commit();
        }


    }
}

