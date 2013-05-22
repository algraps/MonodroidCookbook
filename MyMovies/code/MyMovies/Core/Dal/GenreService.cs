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
using MyMovies.Model;
using SQLite;

namespace MyMovies.Core.Dal
{
    public class GenreService: IGenreService
    {
        private MyMoviesDatabaseHelper _helper;

        public GenreService(Context context)
        {
            _helper = new MyMoviesDatabaseHelper(context);
        }


        public long AddGenre(string description)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database.Insert(new Genre()
                {
                    Description = description,
                });
            }
        }

        public int EditGenre(Genre genre, string descritpion)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                genre.Description = descritpion;
                return database.Update(genre);
            } 
        }

        public List<Genre> GenreList()
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<Genre>()
                        .ToList();
            }
        }

        public int DeleteGenre(Genre genre)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database.Delete(genre);
            }
        }

        public Genre GetGenre(int idGenre)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<Genre>()
                        .ToList().FirstOrDefault(o => o.Id == idGenre);
            }
        }

        public Genre GetGenreByName(string description)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<Genre>()
                        .ToList().FirstOrDefault(o => o.Description.Equals(description));
            }
        }
    }
}