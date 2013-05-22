using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyMovies.Model;

namespace MyMovies
{
    public class GenreListAdapter: BaseAdapter
    {
        private IEnumerable<Genre> _lstGenre;
        private Activity _context;

        public GenreListAdapter(Activity context, IEnumerable<Genre> lstGenre)
        {
            _context = context;
            _lstGenre = lstGenre;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = (convertView
                            ?? _context.LayoutInflater.Inflate(
                                    Resource.Layout.GenreListItem, parent, false)
                        ) as LinearLayout;
            var genre = _lstGenre.ElementAt(position);

            view.FindViewById<TextView>(Resource.Id.tvGenreShow).Text = genre.Description;
            view.FindViewById<TextView>(Resource.Id.tvIdShow).Text = genre.Id.ToString(CultureInfo.InvariantCulture);                
            return view;
        }

        public override int Count
        {
            get { return _lstGenre.Count(); }
        }

        public Genre GetGenre(int position)
        {
            return _lstGenre.ElementAt(position);
        }

        public override Java.Lang.Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return position;
        }
    }
}