using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MakeCall
{
    [Activity(Label = "MakeCall", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.btSend);
            EditText txtNumber = FindViewById<EditText>(Resource.Id.txtNumber);
            button.Click += delegate
                                {
                                    if (string.IsNullOrEmpty(txtNumber.Text))
                                        return;
                                    var callIntent = new Intent(Intent.ActionCall);
                                    callIntent.SetData(Android.Net.Uri.Parse(txtNumber.Text));
                                    StartActivity(callIntent);
                                };
        }
    }
}

