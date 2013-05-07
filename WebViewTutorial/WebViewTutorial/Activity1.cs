using System;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Android.OS;

namespace WebViewTutorial
{
    [Activity(Label = "WebViewTutorial", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        int count = 1;
        private WebView webView;
        private String url = "www.google.com";

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
                       
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            webView = (WebView) FindViewById(Resource.Id.webview);
            webView.LoadUrl(url);

            //Bundle extras = GetIntent().GetBundleExtra();
            //if(extras == null)
            //return;
            //String url = extras.GetString("url");

            webView.Settings.JavaScriptEnabled = true;


            //ProgressDialog progressDialog = new ProgressDialog(this);
            //progressDialog.SetMessage("Waiting ...");
            //progressDialog.SetCancelable(false);
            //progressDialog.Show();

            //progressDialog.SetIndeterminateDrawable((Drawable) Resource.Drawable.Icon);


            //ProgressDialog progressDialog = new ProgressDialog(this);
            //progressDialog.SetMessage("Waiting ...");
            //progressDialog.SetCancelable(false);
            //progressDialog.Show();
            //String url = extras.GetString("url");
            
            
            //webView.SetBackgroundColor(Color.ParseColor("#b02430"));
            //webView.LoadUrl(url);


        }
        
    }

    
}

