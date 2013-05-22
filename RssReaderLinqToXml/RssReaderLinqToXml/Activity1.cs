/*
 * Monodroid - LinqToXML
 * Author: Alessandro Graps
 * Year: 2013
 */
 using System;
using System.IO;
using System.Net;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RssReaderLinqToXml
{
    [Activity(Label = "RssReaderLinqToXml", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        EditText rssUriInput;
        Button button;
        TextView output;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            rssUriInput = FindViewById<EditText>(Resource.Id.rssUriText);
            button = FindViewById<Button>(Resource.Id.myButton);
            output = FindViewById<TextView>(Resource.Id.outputTextView);

            button.Click += new EventHandler(button_Click);
        }

        void button_Click(object sender, EventArgs e)
        {
            output.Text += "user input: " + rssUriInput.Text + "\n";
            
            try
            {
                output.Text += "### document ###";
                //Use LINQ TO XML TO LOAD BLOG URI
                XDocument ourBlog = XDocument.Load(rssUriInput.Text);
                output.Text += ourBlog.ToString();
                output.Text += "### document ###";




            }
            catch (Exception ex)
            {
                output.Text += "OOOPS something went wrong:\n" + ex.ToString();
            }
        }
    }
}

