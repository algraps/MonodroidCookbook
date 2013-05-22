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

namespace MyMovies
{
    [Activity(Label = "Edit Movie")]
    public class MyMovieViewEdit : Activity
    {
        private ImageButton _btSave;
        private ImageButton _btDelete;
        private ImageButton _btBack;

        private TextView _txId;
        private EditText _txTitle;
        private EditText _txYear;
        private Spinner _cbGenre;

        private long _idMovie;
        private int? _idGenre;

        private ProgressDialog _progressDialog;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            try
            {
                SetContentView(Resource.Layout.MyMovieEdit);
                Init();

                _txTitle.Text = Intent.GetStringExtra("Title");
                _txYear.Text = Intent.GetIntExtra("Year",-1).ToString();                
                
                _idMovie = Intent.GetLongExtra("IdMovie", -1);
                _idGenre = Intent.GetIntExtra("IdGenre", -1);
                _txId.Text = _idMovie.ToString();
                var genreList = ((DatabaseApplication)Application).GenereRepo.GenreList();

                // Populate list of account types for phone
                ArrayAdapter<string> adapter;
                adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                foreach (Model.Genre genre in genreList)
                {
                    adapter.Add(genre.Description);
                }
                _cbGenre.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(CbGenre_ItemSelected);
                _cbGenre.Adapter = adapter;

                var genreSel = ((DatabaseApplication)Application).GenereRepo.GetGenre(_idGenre.Value);

                if (genreSel != null)
                {
                    int spinnerPosition = adapter.GetPosition(genreSel.Description);
                    _cbGenre.SetSelection(spinnerPosition);
                }

                _btBack.Click += delegate
                {
                    Finish();
                };

                _btDelete.Click += delegate
                {
                    Model.MyMovie movie = ((DatabaseApplication)Application).MovieRepo.GetMovie(_idMovie);
                    if (movie != null)
                        new AlertDialog.Builder(this)
                           .SetTitle(Resource.String.ApplicationName)
                           .SetMessage("Are you sure?")
                           .SetPositiveButton("Ok", delegate
                           {
                               _progressDialog.Show();
                               ThreadPool.QueueUserWorkItem(state =>
                               {
                                   ((DatabaseApplication)Application).MovieRepo.DeleteMovie(movie);
                                   RunOnUiThread(() => OnSuccessLoad());
                               });
                         

                           })
                           .SetNegativeButton("Cancel", delegate
                           {

                           })
                           .Show();
                };

                _btSave.Click += delegate
                {
                    
                
                    if (!_idGenre.HasValue)
                    {
                        new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify a genre.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                        return;
                    }
                    else if (string.IsNullOrEmpty(_txTitle.Text))
                    {    new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify a title.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                        return;
                    }
                    else if (string.IsNullOrEmpty(_txYear.Text))
                    {
                        new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify the year.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                        return;
                    }
                    else
                    {
                        _progressDialog.Show();
                        ThreadPool.QueueUserWorkItem(state =>
                        {
                            Save();
                            RunOnUiThread(() => OnSuccessLoad());
                        });    
                    }
                                            
                };

            }
            catch (Exception ex)
            {
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
            _cbGenre = FindViewById<Spinner>(Resource.Id.cbGenre);
            _txTitle = FindViewById<EditText>(Resource.Id.txViewTitle);
            _txYear = FindViewById<EditText>(Resource.Id.txViewYear);

            _txId = FindViewById<TextView>(Resource.Id.tvViewId1);

            _btSave = FindViewById<ImageButton>(Resource.Id.imgEdit);
            _btDelete = FindViewById<ImageButton>(Resource.Id.imgDelete);
            _btBack = FindViewById<ImageButton>(Resource.Id.imgBack);

            _progressDialog = new ProgressDialog(this) { Indeterminate = true };
            _progressDialog.SetTitle("Loading movie");
            _progressDialog.SetMessage("Please wait...");

        }

        /// <summary>
        /// Called when [success load].
        /// </summary>
        private void OnSuccessLoad()
        {
            _progressDialog.Hide();
            Finish();

        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        private void Save()
        {
            try
            {
                Model.MyMovie movie = ((DatabaseApplication)Application).MovieRepo.GetMovie(_idMovie);
                ((DatabaseApplication)Application).MovieRepo.EditMovie(movie,
                        _txTitle.Text, Convert.ToInt32(_txYear.Text), _idGenre.Value);
                
            }
            catch (Exception ex)
            {
                new AlertDialog.Builder(this)
                        .SetTitle(Resource.String.ApplicationName)
                        .SetMessage(ex.Message)
                        .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                        .Show();
            }


        }


        /// <summary>
        /// Handles the ItemSelected event of the CbGenre control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="AdapterView.ItemSelectedEventArgs" /> instance containing the event data.</param>
        private void CbGenre_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var genre = ((DatabaseApplication)Application).GenereRepo.GetGenreByName(spinner.GetItemAtPosition(e.Position).ToString());
            _idGenre = genre.Id;
        }
    }
}