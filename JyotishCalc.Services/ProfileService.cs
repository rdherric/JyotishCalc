using System;
using System.Linq;
using JyotishCalc.Data.User;
using JyotishCalc.Services.Base;

namespace JyotishCalc.Services
{
    /// <summary>
    /// ProfileService gets and sets the Profiles for the Application
    /// in the RoamingFolder JSON file.
    /// </summary>
    public class ProfileService : FileServiceBase<Profile>
    {
        #region Constants
        private const string _settingsFile = "JyotishCalc.Data.User.Profiles.json";
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the ProfileService
        /// </summary>
        public ProfileService()
        {
            //Try to get the Profiles
            try
            {
                this.DeserializeListFromSettingsFolder(ProfileService._settingsFile);
            }
            catch
            {
                //File doesn't exist or JSON can't be deserialized
            }
        }
        #endregion


        #region Methods to save or remove Profiles
        /// <summary>
        /// Save adds a new or updates an existing Profile in the 
        /// List and saves it in RoamingFolder.
        /// </summary>
        /// <param name="profile">The Profile to add or update to the RoamingFolder</param>
        public void Save(Profile profile)
        {
            //Try to get the index of the current Profile
            int index = this.All.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and Update if possible.  Add if
            //the Profile does not exist.
            if (index > 0)
            {
                this.All[index] = profile;
            }
            else
            {
                //Set the ID of the Profile to a HashCode
                profile.ID = new Guid();

                //Add the Profile to the List
                this.All.Add(profile);
            }

            //Update the RoamingFolder
            this.UpdateProfiles();
        }


        /// <summary>
        /// Remove removes an existing Profile from the 
        /// List and saves the result to RoamingFolder.
        /// </summary>
        /// <param name="profile">The Profile to remove from the RoamingFolder</param>
        public void Remove(Profile profile)
        {
            //Try to get the index of the current Profile
            int index = this.All.FindIndex(p => p.ID == profile.ID);

            //Check for the Profile and remove if possible
            if (index > 0)
            {
                //Remove the Profile
                this.All.RemoveAt(index);

                //Update the RoamingFolder
                this.UpdateProfiles();
            }
        }
        #endregion


        #region Helper Methods
        /// <summary>
        /// UpdateProfiles sets the value of the Profile List into
        /// the RoamingFolder.
        /// </summary>
        private void UpdateProfiles()
        {
            //Try to save the Profiles
            try
            {
                this.SerializeListToSettingsFolder(ProfileService._settingsFile);
            }
            catch
            {
                //JSON can't be Serialized
            }
        }
        #endregion
    }
}
