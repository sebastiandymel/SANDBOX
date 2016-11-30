using System;
using Android.App;
using Android.Widget;
using Android.OS;

namespace AndroidApp1
{
    [Activity(Label = "AndroidApp1", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            var button = FindViewById<Button>(Resource.Id.button1);
            button.Click += OnButtonClick;
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            // var view = FindViewById<View>(Resource.Id.view1);
            
        }
    }
}

