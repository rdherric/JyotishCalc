using System;
using Jyotish.Data.Position;

namespace Jyotish.Data.User
{
    /// <summary>
    /// Profile stores a Jyotish profile for a User.
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// ID gets and sets the ID of the Profile for creating and
        /// deleting from the Profiles collection.
        /// </summary>
        internal Guid ID { get; set; }

        
        /// <summary>
        /// Name gets and sets the Name of the User that represents 
        /// the Profile.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// BirthDateTime gets and sets the DateTime of the User for 
        /// this Profile.
        /// </summary>
        public DateTime BirthDateTime { get; set; }


        /// <summary>
        /// BirthLocation gets and sets the LatLong of the birth
        /// location of the User for this Profile.
        /// </summary>
        public LatLong BirthLocation { get; set; }
    }
}
