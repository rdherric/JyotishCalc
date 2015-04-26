using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using Windows.Storage;

namespace JyotishCalc.Services.Base
{
    /// <summary>
    /// SqLiteService is used to read and write to a SQLite
    /// database.
    /// </summary>
    public abstract class SqLiteServiceBase<T> where T : class
    {
        #region Member Variables
        private string _fileName = String.Empty;
        protected SQLiteConnection _db = null;
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the SqLiteServiceBase object.
        /// </summary>
        /// <param name="fileName">The name of the file from which to get data</param>
        public SqLiteServiceBase(string fileName)
        {
            //Save the input in member variables
            this._fileName = fileName;

            //If the File name wasn't provided, throw an Exception
            if (String.IsNullOrEmpty(this._fileName) == true)
            {
                throw new ArgumentException("No SQLite database file name was provided to the constructor.", "fileName");
            }

            //Create the Connection to the database
            this._db = new SQLiteConnection(new SQLitePlatformWinRT(), this._fileName);
            this._db.CreateTable<T>();
        }
        #endregion


        #region Public Properties
        /// <summary>
        /// All gets the IEnumerable of T for use by the client.
        /// </summary>
        public List<T> All
        {
            get { return this._db.Table<T>().ToList(); }
        }
        #endregion


        #region Database Methods
        /// <summary>
        /// Insert inserts an Entity into the database
        /// </summary>
        /// <param name="entity">The Entity to insert</param>
        protected void Insert(T entity)
        {
            //Insert the entity
            this._db.Insert(entity);
        }

        
        /// <summary>
        /// Update updates an Entity in the database
        /// </summary>
        /// <param name="entity">The Entity to update</param>
        protected void Update(T entity)
        {
            //Update the entity
            this._db.Update(entity);
        }
        #endregion


        #region Protected Connection Location structs
        /// <summary>
        /// DatabaseName holds the paths of the SQLite
        /// database files.
        /// </summary>
        protected class DatabaseName
        {
            public static string Profile = Path.Combine(ApplicationData.Current.RoamingFolder.Path, "Profiles.sqlite");
            public static string Fixed = Path.Combine(ApplicationData.Current.LocalFolder.Path, "Data", "Fixed.sqlite");
        }
        #endregion
    }
}
