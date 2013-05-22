/*
 * MyMovies App
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyMovies
{
    [Activity(Label = "MyMovies", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private ListView _myMoviesList;

        private ImageButton _btAddMovie;
        private ImageButton _btFindMovie;
        private ImageButton _btAddGenre;

        private ProgressDialog _progressDialog;


        protected override void OnResume()
        {
            base.OnResume();
            //SetContentView(Resource.Layout.Main);
            try
            {
                Init();
                _progressDialog.Show();
                ThreadPool.QueueUserWorkItem(state =>
                    {
                        GetMovies();
                        RunOnUiThread(() => OnSuccessLoad());
                    });
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

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);

                // Set our view from the "main" layout resource
                SetContentView(Resource.Layout.Main);

                Init();

                _btAddMovie.Click += delegate
                {
                    StartActivity(typeof(MyMoviewViewAdd));
                };

                _btAddGenre.Click += delegate
                {
                    StartActivity(typeof(GenreView));
                };

                _btFindMovie.Click += delegate
                {
                    StartActivity(typeof(MyMovieFind));
                };   
                

                _myMoviesList.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(MyMoviesListClick);
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

        #region private
        /// <summary>
        /// Inits this instance.
        /// </summary>
        private void Init()
        {
            _btAddMovie = FindViewById<ImageButton>(Resource.Id.imgAdd);
            _btAddGenre = FindViewById<ImageButton>(Resource.Id.imgAddGenre);    
            _btFindMovie = FindViewById<ImageButton>(Resource.Id.imgFind);
            _myMoviesList = FindViewById<ListView>(Resource.Id.lvMovie);
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
                RunOnUiThread(() => _myMoviesList.Adapter = new MyMoviesListAdapter(this, myMoviesLst, ((DatabaseApplication)Application).GenereRepo));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region event
        /// <summary>
        /// Mies the movies list click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AdapterView.ItemClickEventArgs" /> instance containing the event data.</param>
        void MyMoviesListClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var movie = ((MyMoviesListAdapter)_myMoviesList.Adapter).GetMovie(e.Position);

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

        
        #endregion
    }
}

