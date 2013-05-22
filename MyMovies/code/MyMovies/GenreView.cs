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
using Android.Views;
using Android.Widget;
using MyMovies.Model;

namespace MyMovies
{
    [Activity(Label = "Genre")]
    public class GenreView : Activity
    {
        private ListView _genreList;
        private ProgressDialog _progressDialog;

        private ImageButton _btAddGenre;
        private ImageButton _btEditGenre;
        private ImageButton _btDeleteGenre;
        private ImageButton _btBack;

        private EditText _txDescription;

        private int? _idGenre;


        protected override void OnResume()
        {
            base.OnResume();
            try
            {
                Init();
                _progressDialog.Show();
                ThreadPool.QueueUserWorkItem(state =>
                {
                    GetGenre();
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

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            try
            {
                SetContentView(Resource.Layout.Genre);
                Init();

                _btAddGenre.Click += delegate
                    {
                        _txDescription.Text = string.Empty;
                        _idGenre = null;
                    };

                _btDeleteGenre.Click += delegate
                {

                    if (_idGenre.HasValue)
                    {
                        if (((DatabaseApplication) Application).MovieRepo.GenreExist(_idGenre.Value) != null)
                        {
                            new AlertDialog.Builder(this)
                                .SetTitle(Resource.String.ApplicationName)
                                .SetMessage("The genre is used.")
                                .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                                .Show();                          
                        }
                        else
                        {
                            Genre genre = ((DatabaseApplication)Application).GenereRepo.GetGenre(_idGenre.Value);
                            if (genre != null)
                                new AlertDialog.Builder(this)
                                  .SetTitle(Resource.String.ApplicationName)
                                  .SetMessage("Are you sure?")
                                  .SetPositiveButton("Ok", delegate
                                  {

                                      _progressDialog.Show();
                                      ThreadPool.QueueUserWorkItem(state =>
                                      {
                                          ((DatabaseApplication)Application).GenereRepo.DeleteGenre(genre);
                                          RunOnUiThread(() => GetGenre());
                                          RunOnUiThread(() => _txDescription.Text = string.Empty);
                                          RunOnUiThread(() => _idGenre = null);
                                          RunOnUiThread(() => OnSuccessLoad());
                                      });


                                  })
                                  .SetNegativeButton("Cancel", delegate
                                  {

                                  })
                                  .Show();
                        }

                       
                    }                        
                };

                _btEditGenre.Click += delegate
                {

                    if (string.IsNullOrEmpty(_txDescription.Text))
                    {
                        new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify a description.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                        return;
                    }
                    else
                    {
                        if (_idGenre == null)
                        {
                            Genre genre = ((DatabaseApplication)Application).GenereRepo.GetGenreByName(_txDescription.Text);
                            if (genre != null)
                            {
                                new AlertDialog.Builder(this)
                                    .SetTitle(Resource.String.ApplicationName)
                                    .SetMessage("Genre already exists.")
                                    .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                                    .Show();
                                return;
                            }
                        }

                    }


                    _progressDialog.Show();


                    ThreadPool.QueueUserWorkItem(state =>
                    {
                        if (_idGenre == null)
                            Save();
                        else                        
                           SaveModify();

                        RunOnUiThread(() => GetGenre());
                        RunOnUiThread(() => _txDescription.Text = string.Empty);
                        RunOnUiThread(() => _idGenre = null);
                        RunOnUiThread(() => OnSuccessLoad());
                    });
                };

                _btBack.Click += delegate
                {
                    Finish();
                };

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
        /// Inits this instance.
        /// </summary>
        private void Init()
        {

            _btBack = FindViewById<ImageButton>(Resource.Id.imgBack);
            _btAddGenre = FindViewById<ImageButton>(Resource.Id.imgAdd);
            _btDeleteGenre= FindViewById<ImageButton>(Resource.Id.imgDelete);
            _btEditGenre = FindViewById<ImageButton>(Resource.Id.imgEdit);
            _txDescription = FindViewById<EditText> (Resource.Id.txGenreDesciption);

            _progressDialog = new ProgressDialog(this) { Indeterminate = true };
            _progressDialog.SetTitle("Loading movie");
            _progressDialog.SetMessage("Please wait...");


            // get ListView object instance from resource and add ItemClick, EventHandler.
            _genreList = FindViewById<ListView>(Resource.Id.lvTemp);
            _genreList.ItemClick += new EventHandler<AdapterView.ItemClickEventArgs>(ListView_ItemClick);
        }


        /// <summary>
        /// Gets the genre.
        /// </summary>
        private void GetGenre()
        {
            try
            {
                var genreList = ((DatabaseApplication)Application).GenereRepo.GenreList();
                RunOnUiThread(() => _genreList.Adapter = new GenreListAdapter(this, genreList));
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// Lists the view_ item click.
        /// </summary>
        /// <param name='sender'>
        /// object sender.
        /// </param>
        /// <param name='e'>
        /// ItemClickEventArgs e.
        /// </param>
        void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var genre = ((GenreListAdapter)_genreList.Adapter).GetGenre(e.Position);
            // get TextView object instance from resource layout record_view.axml.            
            TextView tvDescription = e.View.FindViewById<TextView>(Resource.Id.tvGenreDescription);            
            // read value and wirte in EditText object.
            _txDescription.Text = genre.Description;
            _idGenre = genre.Id;
        }

        private void OnSuccessLoad()
        {
            if (_progressDialog.IsShowing)
                _progressDialog.Hide();            
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {
            try
            {
                ((DatabaseApplication)Application).GenereRepo.AddGenre(_txDescription.Text);                
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

        private void SaveModify()
        {
            try
            {
                Genre genre = ((DatabaseApplication)Application).GenereRepo.GetGenre(_idGenre.Value);
                ((DatabaseApplication)Application).GenereRepo.EditGenre(genre,_txDescription.Text);
                
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
    }
}