using System;
using System.Collections.Generic;

namespace JyotishCalculator.Model.User
{
    /// <summary>
    /// Profiles gets and sets the Profiles for the Application
    /// in the LocalSettings.
    /// </summary>
    public class Profiles
    {
        #region Member Variables
        private List<Profile> _profiles = new List<Profile>();
        #endregion


        #region Public Properties
        /// <summary>
        /// All gets the complete List of Profiles from the Collection.
        /// </summary>
        public List<Profile> All
        {
            get 
            {
                //If there are no Profiles, try to get them
                if (this._profiles.Count == 0)
                {
                    //Get from LocalSettings
                }

                //Return the result
                return this._profiles; 
            }
        }
        #endregion


        #region Methods to Add / Remove Profiles
        #endregion
    }
}
