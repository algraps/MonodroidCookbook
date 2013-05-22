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
    public interface IGenreService
    {

        /// <summary>
        /// Adds the genre.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        long AddGenre(string description);

        /// <summary>
        /// Edits the EditGenre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <param name="descritpion">The descritpion.</param>
        /// <returns></returns>
        int EditGenre(Genre genre, string descritpion);

        /// <summary>
        /// Genres the list.
        /// </summary>
        /// <returns></returns>
        List<Genre> GenreList();

        /// <summary>
        /// Deletes the genre.
        /// </summary>
        /// <param name="genre">The genre.</param>
        /// <returns></returns>
        int DeleteGenre(Genre genre);

        /// <summary>
        /// Gets the genre.
        /// </summary>
        /// <param name="idGenre">The id genre.</param>
        /// <returns></returns>
        Genre GetGenre(int idGenre);

        /// <summary>
        /// Gets the name of the genre by.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        Genre GetGenreByName(string description);
    }
}