/*
 * MyMovies App
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyMovies.Core.Dal.Interface;

namespace MyMovies.Core.Dal
{
    class GenreSingleton
    {
        private static volatile IGenreService _genreDao = null;
        private static readonly object GenreSyncRoot = new object();

        public static IGenreService GenreDao(Context context)
        {
            if (_genreDao == null)
            {
                lock (GenreSyncRoot)
                {
                    if (_genreDao == null) // double-check
                        _genreDao = new GenreService(context);
                }
            }
            return _genreDao;
        }
    }
}