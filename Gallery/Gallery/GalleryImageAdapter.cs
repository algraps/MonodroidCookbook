using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

namespace MyGallery
{
    public class GalleryImageAdapter:BaseAdapter
    {
        private Activity context;

        private static ImageView imageView;

        private List<Drawable> plotsImages;

        private static ViewHolder holder;
        public override Object GetItem(int position)
        {
            return null;
        }

        public override long GetItemId(int position)
        {
            return 0; 
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                holder = new ViewHolder();
                imageView = new ImageView(this.context);
                imageView.SetPadding(3, 3, 3, 3);
                convertView = imageView;
                holder.ImageView = imageView;
                convertView.SetTag(Resource.Layout.Main,holder);
            }
            else
            {

                holder = (ViewHolder)convertView.GetTag(Resource.Layout.Main);
            }

            holder.ImageView.SetImageDrawable(plotsImages[position]);

            holder.ImageView.SetScaleType(ImageView.ScaleType.FitXy);
            holder.ImageView.LayoutParameters = new Gallery.LayoutParams(150, 90);

            return imageView;
        }

        public override int Count
        {
            get { return plotsImages.Count; }
        }

        public GalleryImageAdapter(Activity context, List<Drawable> plotsImages)
        {

            this.context = context;
            this.plotsImages = plotsImages;

        }

        private class ViewHolder : Java.Lang.Object
        {
            public ImageView ImageView;
        }
    }

}