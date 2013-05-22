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
    [Activity(Label = "Add Movie")]
    public class MyMoviewViewAdd : Activity
    {
        private ImageButton _btSave;
        private ImageButton _btBack;
        
        private EditText _txTitle;
        private EditText _txYear;
        private Spinner _cbGenre;
        
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
                SetContentView(Resource.Layout.MyMovieAdd);
                Init();

                _txTitle.Text = string.Empty;
                _txYear.Text = string.Empty;
                
                
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

                _btBack.Click += delegate
                {
                    Finish();
                };                

                _btSave.Click += delegate
                {
                    Model.MyMovie movie = ((DatabaseApplication)Application).MovieRepo.GetMovie(_txTitle.Text);

                    if (!_idGenre.HasValue)
                        new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify a genre.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                    else if (movie != null)
                        new AlertDialog.Builder(this)
                           .SetTitle(Resource.String.ApplicationName)
                           .SetMessage("Movie alredy exists.")
                           .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                           .Show();
                    else if (string.IsNullOrEmpty(_txTitle.Text))
                        new AlertDialog.Builder(this)
                            .SetTitle(Resource.String.ApplicationName)
                            .SetMessage("Specify a title.")
                            .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                            .Show();
                    else if (string.IsNullOrEmpty(_txYear.Text))
                        new AlertDialog.Builder(this)
                           .SetTitle(Resource.String.ApplicationName)
                           .SetMessage("Specify the year.")
                           .SetPositiveButton("Ok", null as IDialogInterfaceOnClickListener)
                           .Show();
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

            _btSave = FindViewById<ImageButton>(Resource.Id.imgEdit);
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
                ((DatabaseApplication)Application).MovieRepo.AddMovie(_txTitle.Text, Convert.ToInt32(_txYear.Text), _idGenre.Value);                
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