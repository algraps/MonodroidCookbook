using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace MyGallery
{
    [Activity(Label = "Gallery", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        private ImageView selectedImageView;
        private ImageView leftArrowImageView;
        private ImageView rightArrowImageView;
        private Gallery gallery;
        private int selectedImagePosition = 0;
        private List<Drawable> drawables;
        private GalleryImageAdapter galImageAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.Main);
            
            GetDrawablesList();
            SetupUI();
        }

        private void SetupUI()
        {
            selectedImageView = (ImageView)FindViewById(Resource.Id.selected_imageview);
            leftArrowImageView = (ImageView)FindViewById(Resource.Id.left_arrow_imageview);
            rightArrowImageView = (ImageView)FindViewById(Resource.Id.right_arrow_imageview);
            gallery = (Gallery)FindViewById(Resource.Id.gallery);


            leftArrowImageView.Click += (sender, e) =>
            {
                if (selectedImagePosition > 0)
                {
                    --selectedImagePosition;

                }

                gallery.SetSelection(selectedImagePosition, false);
            };

            rightArrowImageView.Click += (sender, e) =>
            {
                if (selectedImagePosition < drawables.Count - 1)
                {
                    ++selectedImagePosition;

                }

                gallery.SetSelection(selectedImagePosition, false);
            };

            gallery.ItemSelected += (sender, e) =>
                {
                    selectedImagePosition = e.Position;

                    if (selectedImagePosition > 0 && selectedImagePosition < drawables.Count - 1)
                    {

                        leftArrowImageView.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.arrow_left_enabled));
                        rightArrowImageView.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.arrow_right_enabled));

                    }
                    else if (selectedImagePosition == 0)
                    {

                        leftArrowImageView.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.arrow_left_disabled));

                    }
                    else if (selectedImagePosition == drawables.Count - 1)
                    {

                        rightArrowImageView.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.arrow_right_disabled));
                    }

                    ChangeBorderForSelectedImage(selectedImagePosition);
                    SetSelectedImage(selectedImagePosition);
                };
            
            galImageAdapter = new GalleryImageAdapter(this, drawables);

            gallery.Adapter = galImageAdapter;

            if (drawables.Count > 0)
            {

                gallery.SetSelection(selectedImagePosition, false);

            }

            if (drawables.Count == 1)
            {

                rightArrowImageView.SetImageDrawable(Resources.GetDrawable(Resource.Drawable.arrow_right_disabled));
            }
        }

        private void ChangeBorderForSelectedImage(int selectedItemPos)
        {

            int count = gallery.ChildCount;
            ImageView imageView;
            for (int i = 0; i < count; i++)
            {

                imageView = (ImageView)gallery.GetChildAt(i);
                imageView.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.image_border));
                imageView.SetPadding(3, 3, 3, 3);

            }

            imageView = (ImageView)gallery.SelectedView;
            imageView.SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.selected_image_border));
            imageView.SetPadding(3, 3, 3, 3);
        }

        private void GetDrawablesList()
        {
            drawables = new List<Drawable>();
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage1));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage2));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage3));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage4));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage5));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage6));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage7));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage8));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage9));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage10));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage11));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage12));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage13));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage14));
            drawables.Add(Resources.GetDrawable(Resource.Drawable.natureimage15));

        }

        private void SetSelectedImage(int selectedImagePosition)
        {

            BitmapDrawable bd = (BitmapDrawable)drawables[selectedImagePosition];
            Bitmap b = Bitmap.CreateScaledBitmap(bd.Bitmap, (int)(bd.IntrinsicHeight * 0.9), (int)(bd.IntrinsicWidth * 0.7), false);
            selectedImageView.SetImageBitmap(b);
            selectedImageView.SetScaleType(ImageView.ScaleType.FitXy);

        }
    }
}

