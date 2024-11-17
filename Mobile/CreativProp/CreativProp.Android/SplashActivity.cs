
using System;
using System.Runtime.Remoting.Contexts;
using Android.App;
using Android.Content.Res;
using Android.OS;

namespace CreativProp.Droid
{
    [Activity(Label = "SplashActivity", Theme = "@style/LaunchTheme")]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_splash);
        }
    }
}

