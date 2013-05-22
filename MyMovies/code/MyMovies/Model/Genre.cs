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
    /// Genre
    /// </summary>
    public class Genre
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Description { get; set; }       
    }
}