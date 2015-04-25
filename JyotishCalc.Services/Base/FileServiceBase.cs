using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Windows.Storage;

namespace JyotishCalc.Services.Base
{
    /// <summary>
    /// FileService is used to read and write files to Settings
    /// and Data folders.
    /// </summary>
    public abstract class FileServiceBase<T> where T : class
    {
        #region Member Variables
        private List<T> _entities = null;
        #endregion


        #region Public Properties
        /// <summary>
        /// All gets the IEnumerable of T for use by the client.
        /// </summary>
        public List<T> All
        {
            get { return this._entities; }
        }
        #endregion


        #region Serialization Methods
        /// <summary>
        /// SerializeListToSettingsFolder serializes the List of T and
        /// writes it to a file.
        /// </summary>
        /// <param name="fileName">The file to which to write</param>
        protected async void SerializeListToSettingsFolder(string fileName)
        {
            //Serialize to RoamingFolder
            this.WriteToStorageFile(
                await ApplicationData.Current.RoamingFolder.CreateFileAsync(
                fileName, CreationCollisionOption.ReplaceExisting));
        }
        #endregion


        #region Deserialization Methods
        /// <summary>
        /// DeserializeListFromSettingsFolder reads a file and deserializes
        /// the contents into a List of T.
        /// </summary>
        /// <param name="fileName">The file from which to read</param>
        protected async void DeserializeListFromSettingsFolder(string fileName)
        {
            //Deserialize from RoamingFolder
            this.ReadFromStorageFile(
                await ApplicationData.Current.RoamingFolder.GetFileAsync(fileName));
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// ReadFromStorageFile reads the contents of a StorageFile
        /// and deserializes it into the member variable List.
        /// </summary>
        /// <param name="file">The StorageFile to read</param>
        private async void ReadFromStorageFile(StorageFile file)
        {
            //If the file came back, use it
            if (file != null)
            {
                //Read the file into a Stream
                using (Stream stream = (await file.OpenReadAsync()).AsStreamForRead())
                {
                    //Get a StreamReader
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        //Deserialize the JSON
                        List<T> temp = JsonConvert.DeserializeObject<List<T>>(reader.ReadToEnd());

                        //Save the values if possible
                        if (temp != null)
                        {
                            this._entities = temp;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// WriteToStorageFile writes the List of T to the 
        /// specified StorageFile.
        /// </summary>
        /// <param name="file">The StorageFile to which to write</param>
        private async void WriteToStorageFile(StorageFile file)
        {
            //If the file came back, use it
            if (file != null)
            {
                //Open the file for writing
                using (Stream stream = await file.OpenStreamForWriteAsync())
                {
                    //Get a StreamWriter
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        //Serialize the JSON
                        writer.Write(JsonConvert.SerializeObject(this._entities));
                    }
                }
            }
        }
        #endregion
    }
}
