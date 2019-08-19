using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FourWays.Core;
using MvvmCross.Platforms.Android.Core;
using MvvmCross.Platforms.Android.Views;

namespace TipCrossDroid.Views
{
    [Activity(
     Label = "@string/app_name",
     MainLauncher = true,
     Icon = "@drawable/icon",
     Theme = "@style/Theme.Splash",
     NoHistory = true,
     ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashView : MvxSplashScreenActivity<MvxAndroidSetup<App>, App>
    {
        public SplashView() : base(Resource.Layout.SplashPage)
        {
        }
    }

}