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
using SQLite;

namespace MyMovies.Model
{
    /// <summary>
    /// MyMovie
    /// </summary>
    public class MyMovie
    {
        [PrimaryKey, AutoIncrement]
        public long IdMovies { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public int IdGenre { get; set; }        
    }
}