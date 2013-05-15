using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.IO;

namespace MyRssReader
{
    [Activity(Label = "MyRssReader", MainLauncher = true, Icon = "@drawable/icon")]
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
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            

            

            



            button.Click += (sender, e) =>
            {
                string url = "http://nothingnessit.wordpress.com/feed/";
                IList feed = PostManager.GetBlogPosts(url, 10);

                TextView textView = FindViewById<Button>(Resource.Id.textTop);
                foreach (Post post in feed)
                {
                    textView.Text += post.Title + "\n";

                }
            };
        }
        
        public bool Ext()
        {
            string fileName = "file.txt";
            string state = Android.OS.Environment.ExternalStorageState;
            try
            {
                Java.IO.File root = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DataDirectory.ToString()), "name");
                if (!root.Exists())
                {
                    root.Mkdirs();
                }
                if (root.CanWrite())
                {
                    Byte[]byteArray = new byte[10];
                    Java.IO.File file = new Java.IO.File(root, fileName);
                    FileOutputStream fos = new FileOutputStream(file);
                    fos.Write(byteArray);
                    fos.Flush();
                    fos.Close();
                }
                return true;
            }
            catch (Java.IO.IOException e)
            {
                return false;
            }
        }
       
    }


}

