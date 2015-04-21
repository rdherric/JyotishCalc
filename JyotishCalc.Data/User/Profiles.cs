using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;

namespace JyotishCalc.Data.User
{
    /// <summary>
    /// Profiles gets and sets the Profiles for the Application
    /// in the LocalSettings.
    /// </summary>
    public class Profiles
    {
        #region Constants
        private const string _settingsFile = "JyotishCalc.Data.User.Profiles.json";
        #endregion


        #region Public Properties
        /// <summary>
        /// All gets the complete List of Profiles from the Collection.
        /// </summary>
        public static async Task<IEnumerable<Profile>> GetAll()
        {
            //Declare a variable to return
            IEnumerable<Profile> rtn = new List<Profile>();

            //Try to get the Profiles
            try
            {
                //Get the file
                StorageFile file = await ApplicationData.Current.RoamingFolder
                    .GetFileAsync(Profiles._settingsFile);

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
                            List<Profile> temp = JsonConvert.DeserializeObject<List<Profile>>(reader.ReadToEnd());

                            //Return if possible
                            if (temp != null)
                            {
                                rtn = temp;
                            }
                        }
                    }
                }
            }
            catch
            {
                //File doesn't exist or JSON can't be deserialized
            }

            //Return the result
            return rtn;
        }
        #endregion


        #region Methods to save or remove Profiles
        /// <summary>
        /// Save adds a new or updates an existing Profile in the 
        /// List and saves it in RoamingSettings.
        /// </summary>
        /// <param name="profile">The Profile to add or update to the RoamingSettings</param>
        public static async void Save(Profile profile)
        {
            //Get the IEnumerable of Profiles
            List<Profile> profiles = new List<Profile>(await Profiles.GetAll());

            //Try to get the index of the current Profile
            int index = profiles.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and Update if possible.  Add if
            //the Profile does not exist.
            if (index > 0)
            {
                profiles[index] = profile;
            }
            else
            {
                //Set the ID of the Profile to a HashCode
                profile.ID = new Guid();

                //Add the Profile to the List
                profiles.Add(profile);
            }

            //Update the RoamingFolder
            Profiles.UpdateProfiles(profiles);
        }


        /// <summary>
        /// Remove removes an existing Profile from the 
        /// List and saves the result to RoamingSettings.
        /// </summary>
        /// <param name="profile">The Profile to remove from the RoamingSettings</param>
        public static async void Remove(Profile profile)
        {
            //Get the IEnumerable of Profiles
            List<Profile> profiles = new List<Profile>(await Profiles.GetAll());

            //Try to get the index of the current Profile
            int index = profiles.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and remove if possible
            if (index > 0)
            {
                //Remove the Profile
                profiles.RemoveAt(index);

                //Update the RoamingFolder
                Profiles.UpdateProfiles(profiles);
            }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// UpdateProfiles sets the value of the Profile List into
        /// the RoamingFolder.
        /// </summary>
        /// <param name="profiles">The List of Profiles to persist</param>
        private static async void UpdateProfiles(List<Profile> profiles)
        {
            //Try to save the Profiles
            try
            {
                //Get the file
                StorageFile file = await ApplicationData.Current.RoamingFolder
                    .CreateFileAsync(Profiles._settingsFile, CreationCollisionOption.ReplaceExisting);

                //If the file came back, use it
                if (file != null)
                {
                    //Read the file into a Stream
                    using (Stream stream = await file.OpenStreamForWriteAsync())
                    {
                        //Get a StreamWriter
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            //Serialize the JSON
                            writer.Write(JsonConvert.SerializeObject(profiles));
                        }
                    }
                }
            }
            catch
            {
                //JSON can't be Serialized
            }
        }
        #endregion
    }
}
