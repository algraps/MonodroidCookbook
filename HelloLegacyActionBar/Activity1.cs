using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Object = Java.Lang.Object;

namespace HelloLegacyActionBar
{
    [Activity (Label = "HelloLegacyActionBar", MainLauncher = true)]
    public class Activity1 : Activity
    {   
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);
            var textView = this.FindViewById<TextView>(Resource.Id.textView1);
            InitializeActionBar();
            textView.Text = "Alternative use of a Spinner";
        }


        private void InitializeActionBar()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.List;

            ActionBar.SetListNavigationCallbacks(
                new NavigationSpinnerAdapter(this),
                new NavigationListener());
        }        

        class NavigationSpinnerAdapter : BaseAdapter
        {
            private List<Java.Lang.Object> _spinnerItems;
            private LayoutInflater _layoutInflater;

            public NavigationSpinnerAdapter(Context context)
            {
                _spinnerItems = new List<Java.Lang.Object>();

                // Create java strings for this sample.
                // This saves a bit on JNI handles.
                _spinnerItems.Add(new Java.Lang.String("Sample item 1"));
                _spinnerItems.Add(new Java.Lang.String("Sample item 2"));
                _spinnerItems.Add(new Java.Lang.String("Sample item 3"));

                // Retrieve the layout inflater from the provided context
                _layoutInflater = LayoutInflater.FromContext(context);
            }

            public override Object GetItem(int position)
            {
                return _spinnerItems[position];
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = convertView;

                // Try to reuse views as much as possible.
                // It is alot faster than inflating new views all the time
                // and it saves quite a bit on memory usage aswell.
                if (view == null)
                {
                    // inflate a new layout for the view.
                    view = _layoutInflater.Inflate(Resource.Layout.SpinnerItem, parent, false);
                }

                var textView = view.FindViewById<TextView>(Resource.Id.sampleTextView1);
                textView.Text = _spinnerItems[position].ToString();

                return view;
            }

            public override int Count
            {
                get { return _spinnerItems.Count; }
            }
        }

        public class NavigationListener : Java.Lang.Object, ActionBar.IOnNavigationListener
        {
            public bool OnNavigationItemSelected(int itemPosition, long itemId)
            {
                //TODO: Handle list selection

                return false;
            }
        }


    }
}


