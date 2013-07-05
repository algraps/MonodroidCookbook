using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
/*
 * Send SMS App
 * Author: Alessandro Graps
 * Year: 2013
 */
using Android.Telephony;

namespace SendSMS
{
    [Activity(Label = "SendSMS", MainLauncher = true, Icon = "@drawable/icon")]
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
                                    try
                                    {
                                        SmsManager smsManager = SmsManager.Default;
                                        smsManager.SendTextMessage(txtNumber.Text,null,"SMS Test!",null,null);
                                        AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
                                        alertDialogBuilder.SetMessage("SMS is sent.");
                                        alertDialogBuilder.Create();
                                        alertDialogBuilder.Show();
                                    }
                                    catch (Exception exception)
                                    {
                                        
                                        throw;
                                    }
                                };
        }
    }
}

