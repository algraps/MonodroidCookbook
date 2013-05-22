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
    class MyMovieSingleton
    {
        private static volatile IMyMovieService _myMovieDao = null;
        private static readonly object MyMovieSyncRoot = new object();

        public static IMyMovieService MyMovieDao(Context context)
        {
            if (_myMovieDao == null)
            {
                lock (MyMovieSyncRoot)
                {
                    if (_myMovieDao == null) // double-check
                        _myMovieDao = new MyMovieService(context);
                }
            }
            return _myMovieDao;
        }
    }
}