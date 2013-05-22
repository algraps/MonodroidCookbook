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

namespace MyMovies
{
    public class MyMoviesListAdapter: BaseAdapter
    {        
        private List<MyMovie> _items;
        private List<MyMovie> _lstMovie;
        private Activity _context;        
        private IGenreService _genreRepo;


        public MyMoviesListAdapter(Activity context, List<MyMovie> lstMovie, IGenreService genreRepo)
        {
            _context = context;
            _lstMovie = lstMovie;
            _items = lstMovie;
            _genreRepo = genreRepo;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (convertView ?? _context.LayoutInflater.Inflate(
                                    Resource.Layout.MovieListItem, parent, false)
                        ) as RelativeLayout;
            var movie = _items.ElementAt(position);

            view.FindViewById<TextView>(Resource.Id.title).Text = movie.Title;
            view.FindViewById<TextView>(Resource.Id.year).Text = movie.Year.ToString();
            view.FindViewById<TextView>(Resource.Id.genre).Text = _genreRepo.GetGenre(movie.IdGenre).Description;

                
            return view;
        }

        public override int Count
        {
            get { return _items.Count(); }
        }

        public MyMovie GetMovie(int position)
        {
            return _items.ElementAt(position);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public void FilterMovie(string filter)
        {
            // Create new empty list to add matched elements to
            List<MyMovie> filtered = new List<MyMovie>();
            // examine each element to build filtered list
            // remember to always use your original items list
            if (string.IsNullOrEmpty(filter))
            {
                filtered = _lstMovie;
            }
            else
            {
                foreach (MyMovie s in this._lstMovie)
                {
                    string name = s.Title;
                    if (name.Contains(filter))
                    {
                        filtered.Add(s);
                    }
                }
            }
            
            //set new (filterd) current list of items
            this._items = filtered;
            //notify ListView to Rebuild
            NotifyDataSetChanged();
        }
    }
}