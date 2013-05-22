/*
 * MyMovies App
 * Author: Alessandro Graps
 * Year: 2013
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Environment = System.Environment;

namespace MyMovies.Core
{
    public class MyMoviesDatabaseHelper : SQLiteOpenHelper
    {
        private const string DATABASE_NAME = "myMoviesdb";
        private const int DATABASE_VERSION = 1;

        public MyMoviesDatabaseHelper(Context context)
            : base(context, DATABASE_NAME, null, DATABASE_VERSION)
        {

        }

        /// <summary>
        /// Called when the database is created for the first time.
        /// </summary>
        /// <param name="db">The database.</param>
        /// <remarks>
        /// 	<para>Called when the database is created for the first time. This is where the
        /// creation of tables and the initial population of the tables should happen.</para>
        /// 	<para>
        /// 		<format type="text/html">
        /// 			<a href="http://developer.android.com/reference/android/database/sqlite/SQLiteOpenHelper.html#onCreate(android.database.sqlite.SQLiteDatabase)">[Android Documentation]</a>
        /// 		</format>
        /// 	</para>
        /// </remarks>
        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(@"
                        CREATE TABLE Genre (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Description TEXT NOT NULL                        
                        )");

            db.ExecSQL(@"
                        CREATE TABLE MyMovie (
                            IdMovies INTEGER PRIMARY KEY AUTOINCREMENT,
                            Title TEXT NOT NULL,Year INTEGER NOT NULL,IdGenre INTEGER NOT NULL                        
                        )");

        }

        /// <summary>
        /// The SQLite ALTER TABLE documentation can be found
        /// .
        /// </summary>
        /// <param name="db">The database.</param>
        /// <param name="oldVersion">The old database version.</param>
        /// <param name="newVersion">The new database version.</param>
        /// <since version="API Level 1" />
        /// <remarks>
        ///   <para>Called when the database needs to be upgraded. The implementation
        /// should use this method to drop tables, add tables, or do anything else it
        /// needs to upgrade to the new schema version.
        ///   <para>
        /// The SQLite ALTER TABLE documentation can be found
        /// . If you add new columns
        /// you can use ALTER TABLE to insert them into a live table. If you rename or remove columns
        /// you can use ALTER TABLE to rename the old table, then create the new table and then
        /// populate the new table with the contents of the old table.
        ///   </para><para>
        /// This method executes within a transaction.  If an exception is thrown, all changes
        /// will automatically be rolled back.
        ///   </para></para>
        ///   <para>
        ///   <format type="text/html">
        ///   <a href="http://developer.android.com/reference/android/database/sqlite/SQLiteOpenHelper.html#onUpgrade(android.database.sqlite.SQLiteDatabase, int, int)" target="_blank">[Android Documentation]</a>
        ///   </format>
        ///   </para>
        /// </remarks>
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            db.ExecSQL("DROP TABLE IF EXISTS Genre");
            db.ExecSQL("DROP TABLE IF EXISTS MyMovie");

            OnCreate(db);

        }
    }
}