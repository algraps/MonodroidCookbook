/*
 * MyMovies App
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Widget;
using Java.Lang;
using MyMovies.Core;
using Exception = System.Exception;

namespace MyMovies
{
    [Activity(Label = "Find", MainLauncher = false)]
   
    public class MyMovieFind : Activity
    {
        EditText _textView;
        ListView _listView;
        MyMoviesListAdapter _adapter;
        private ProgressDialog _progressDialog;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate(bundle);
            try
            {               
                SetContentView(Resource.Layout.FindMovie);
                Init();
                _progressDialog.Show();
                ThreadPool.QueueUserWorkItem(state =>
                {
                    GetMovies();
                    RunOnUiThread(() => OnSuccessLoad());
                });
                _textView.TextChanged += delegate(object sender, Android.Text.TextChangedEventArgs e)
                {
                    _adapter.FilterMovie(e.Text.ToString());
                };
                _listView.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(MyMoviesListClick);
            }
            catch (Exception ex)
            {
                if (_progressDialog.IsShowing)
                    _progressDialog.Hide();
                new AlertDialog.Builder(this)
                        .SetTitle(Resource.String.ApplicationName)
                        .SetMessage(ex.Message)
                        .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                        .Show();
            }
            
        }

        /// <summary>
        /// Mies the movies list click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AdapterView.ItemClickEventArgs" /> instance containing the event data.</param>
        void MyMoviesListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var movie = ((MyMoviesListAdapter)_listView.Adapter).GetMovie(e.Position);

            var intent = new Intent();
            intent.SetClass(this, typeof(MyMovieViewEdit));
            intent.PutExtra("Title", movie.Title);
            intent.PutExtra("IdGenre", movie.IdGenre);
            intent.PutExtra("IdMovie", movie.IdMovies);
            intent.PutExtra("Year", movie.Year);

            StartActivity(intent);
        }

        /// <summary>
        /// Called when [success load].
        /// </summary>
        private void OnSuccessLoad()
        {
            _progressDialog.Hide();

        }


        /// <summary>
        /// Inits this instance.
        /// </summary>
        private void Init()
        {
            _textView = this.FindViewById<EditText>(Resource.Id.text);
            _listView = this.FindViewById<ListView>(Resource.Id.list);
            _progressDialog = new ProgressDialog(this) { Indeterminate = true };
            _progressDialog.SetTitle("Loading movie");
            _progressDialog.SetMessage("Please wait...");
        }

        /// <summary>
        /// Gets the movies.
        /// </summary>
        private void GetMovies()
        {
            try
            {
                var myMoviesLst = ((DatabaseApplication)Application).MovieRepo.MyMovieList();
                _adapter = new MyMoviesListAdapter(this, myMoviesLst, ((DatabaseApplication)Application).GenereRepo);
                RunOnUiThread(() => _listView.Adapter = _adapter);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


    }

}