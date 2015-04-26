using System;
using System.Linq;
using JyotishCalc.Model.User;
using JyotishCalc.Services.Base;

namespace JyotishCalc.Services
{
    /// <summary>
    /// ProfileService gets and sets the Profiles for the Application
    /// in the RoamingFolder JSON file.
    /// </summary>
    public class ProfileService : SqLiteServiceBase<Profile>
    {
        #region Constants
        private const string _settingsFile = "JyotishCalc.Data.User.Profiles.json";
        #endregion


        #region Constructor
        /// <summary>
        /// Default Constructor for the ProfileService.
        /// </summary>
        public ProfileService() : base(DatabaseName.Profile) { }
        #endregion
    }
}
