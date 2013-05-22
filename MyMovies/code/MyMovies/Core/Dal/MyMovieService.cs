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
    public class MyMovieService: IMyMovieService
    {
        private MyMoviesDatabaseHelper _helper;

        public MyMovieService(Context context)
        {
            _helper = new MyMoviesDatabaseHelper(context);
        }

        public long AddMovie(string title, int year, int idGenre)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database.Insert(new MyMovie()
                {
                    Title = title,
                    Year = year,
                    IdGenre = idGenre
                });
            }
        }

        public int EditMovie(MyMovie mymovie, string title, int year, int idGenre)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                mymovie.Title = title;
                mymovie.Year = year;
                mymovie.IdGenre = idGenre;
                return database.Update(mymovie);
            } 
        }

        public List<MyMovie> MyMovieList()
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<MyMovie>()
                        .ToList();
            }
        }

        public int DeleteMovie(MyMovie movies)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database.Delete(movies);
            }
        }

        public MyMovie GetMovie(string search)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<MyMovie>()
                        .ToList().FirstOrDefault(o => o.Title.Equals(search));
            }
        }

        public List<MyMovie> FindMovie(string search)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<MyMovie>()
                        .Where(o => o.Title.Contains(search)).ToList();
            }
        }

        public MyMovie GetMovie(long idMovies)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<MyMovie>()
                        .ToList().FirstOrDefault(o => o.IdMovies.Equals(idMovies));
            }
        }


        public MyMovie GenreExist(int idGenre)
        {
            using (var database = new SQLiteConnection(_helper.WritableDatabase.Path))
            {
                return database
                        .Table<MyMovie>()
                        .ToList().FirstOrDefault(o => o.IdGenre.Equals(idGenre));
            }
        }
    }
}