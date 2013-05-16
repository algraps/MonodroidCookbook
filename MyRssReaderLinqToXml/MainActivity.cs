using System;
using System.Xml.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyRssReaderLinqToXml
{
    [Activity(Label = "MyRssReader", MainLauncher = true, Icon = "@drawable/icon")]
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
                output.Text += XDocument.Load(rssUriInput.Text).ToString();
                output.Text += "### document ###";
            }
            catch (Exception ex)
            {
                output.Text += "OOOPS something went wrong:\n" + ex.ToString();
            }
        }

	}
}


