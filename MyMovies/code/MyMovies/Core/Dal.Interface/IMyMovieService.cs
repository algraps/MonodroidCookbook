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
using MyMovies.Model;

namespace MyMovies.Core.Dal.Interface
{
    public interface IMyMovieService
    {
        /// <summary>
        /// Adds the movie.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="year">The year.</param>
        /// <param name="idGenre">The id genre.</param>
        /// <returns></returns>
        long AddMovie(string title, int year, int idGenre);

        /// <summary>
        /// Edits the movie.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="title">The title.</param>
        /// <param name="year">The year.</param>
        /// <param name="idGenre">The id genre.</param>
        /// <returns></returns>
        int EditMovie(MyMovie account, string title, int year, int idGenre);

        /// <summary>
        /// Mies the movie list.
        /// </summary>
        /// <returns></returns>
        List<MyMovie> MyMovieList();

        /// <summary>
        /// Deletes the movie.
        /// </summary>
        /// <param name="movies">The movies.</param>
        /// <returns></returns>
        int DeleteMovie(MyMovie movies);

        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        MyMovie GetMovie(string search);

        /// <summary>
        /// Finds the movie.
        /// </summary>
        /// <param name="search">The search.</param>
        /// <returns></returns>
        List<MyMovie> FindMovie(string search);

        /// <summary>
        /// Gets the movie.
        /// </summary>
        /// <param name="idMovies">The id movies.</param>
        /// <returns></returns>
        MyMovie GetMovie(long idMovies);

        /// <summary>
        /// Genres the exist.
        /// </summary>
        /// <param name="idGenre">The id genre.</param>
        /// <returns></returns>
        MyMovie GenreExist(int idGenre);
    }
}