/*
 * Contacts App
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Uri = Android.Net.Uri;
namespace Contacts
{
    [Activity(Label = "AndroidApplication1", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private ListView _list;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            _list = FindViewById<ListView>(Resource.Id.contact_list);

            var contacts = ManagedQuery(ContactsContract.Contacts.ContentUri, null, null, null, null);

            _list.Adapter =
                new SimpleCursorAdapter(
                    this,
                    Resource.Layout.item,
                    contacts,
                    new string[] { ContactsContract.ContactsColumnsConsts.DisplayName },
                    new int[] { Resource.Id.contact_name });
            _list.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(_list_ItemClick);
        }

        private void _list_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var item = (ICursor)_list.Adapter.GetItem(e.Position);
            int id = item.GetInt(item.GetColumnIndex("_id"));
            var intent = new Intent(Intent.ActionView);
            var uri = Uri.WithAppendedPath(ContactsContract.Contacts.ContentUri, id.ToString());

            intent.SetData(uri);
            StartActivity(intent);
        }

    }
}

