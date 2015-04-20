using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace Jyotish.Data.User
{
    /// <summary>
    /// Profiles gets and sets the Profiles for the Application
    /// in the LocalSettings.
    /// </summary>
    public class Profiles
    {
        #region Constants
        private const string _settingsKey = "Jyotish.Data.User.Profiles";
        private static ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;
        private static List<Profile> _profiles = new List<Profile>();
        #endregion


        #region Public Properties
        /// <summary>
        /// All gets the complete List of Profiles from the Collection.
        /// </summary>
        public static IEnumerable<Profile> All
        {
            get 
            {
                //If there are no Profiles, try to get them
                if (Profiles._profiles.Count == 0)
                {
                    //Get from LocalSettings
                    if (Profiles._settings.Values.ContainsKey(Profiles._settingsKey) == true)
                    {
                        Profiles._profiles = Profiles._settings.Values[Profiles._settingsKey] as List<Profile>;
                    }
                }

                //Return the result as a copy of the List
                return Profiles._profiles.ToList(); 
            }
        }
        #endregion


        #region Methods to Add / Update / Delete Profiles
        /// <summary>
        /// AddUpdate adds a new or updates an existing Profile in the 
        /// List and saves it in RoamingSettings.
        /// </summary>
        /// <param name="profile">The Profile to add or update to the RoamingSettings</param>
        public static void AddUpdate(Profile profile)
        {
            //Try to get the index of the current Profile
            int index = Profiles._profiles.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and Update if possible.  Add if
            //the Profile does not exist.
            if (index > 0)
            {
                Profiles._profiles[index] = profile;
            }
            else
            {
                //Set the ID of the Profile to a HashCode
                profile.ID = new Guid();

                //Add the Profile to the List
                Profiles._profiles.Add(profile);
            }

            //Update the RoamingSettings
            Profiles.UpdateSettings();
        }


        /// <summary>
        /// Remove removes an existing Profile from the 
        /// List and saves the result to RoamingSettings.
        /// </summary>
        /// <param name="profile">The Profile to remove from the RoamingSettings</param>
        public static void Remove(Profile profile)
        {
            //Try to get the index of the current Profile
            int index = Profiles._profiles.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and remove if possible
            if (index > 0)
            {
                //Remove the Profile
                Profiles._profiles.RemoveAt(index);

                //Update the RoamingSettings
                Profiles.UpdateSettings();
            }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// UpdateSettings sets the value of the Profile List into
        /// RoamingSettings.
        /// </summary>
        private static void UpdateSettings()
        {
            //Flush the Profiles to RoamingSettings
            Profiles._settings.Values.Remove(Profiles._settingsKey);
            Profiles._settings.Values.Add(Profiles._settingsKey, Profiles._profiles);
        }
        #endregion
    }
}
