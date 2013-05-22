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
using MyMovies.Core.Dal;
using MyMovies.Core.Dal.Interface;

namespace MyMovies
{
    [Application]
    public class DatabaseApplication : Application
    {
        public IGenreService GenereRepo { get; set; }
        public IMyMovieService MovieRepo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseApplication" /> class.
        /// </summary>
        /// <param name="handle">The handle.</param>
        public DatabaseApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {

        }

        /// <summary>
        /// Called when the application is starting, before any other application
        /// objects have been created.
        /// </summary>
        /// <since version="API Level 1" />
        /// <remarks>
        ///   <para>Called when the application is starting, before any other application
        /// objects have been created.  Implementations should be as quick as
        /// possible (for example using lazy initialization of state) since the time
        /// spent in this function directly impacts the performance of starting the
        /// first activity, service, or receiver in a process.
        /// If you override this method, be sure to call super.onCreate().
        ///   </para>
        ///   <para>
        ///   <format type="text/html">
        ///   <a href="http://developer.android.com/reference/android/app/Application.html#onCreate()" target="_blank">[Android Documentation]</a>
        ///   </format>
        ///   </para>
        /// </remarks>
        public override void OnCreate()
        {
            base.OnCreate();

            //NoteRepository = new StandardNoteRepository(this);
            GenereRepo = new GenreService(this);
            MovieRepo = new MyMovieService(this);
        }
    }
}