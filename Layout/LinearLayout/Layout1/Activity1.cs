/*
 * Monodroid - Linera Layout
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Layout1
{
    [Activity(Label = "Layout1", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

        }
    }
}

