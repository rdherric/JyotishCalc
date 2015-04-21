using System;

namespace JyotishCalc.Data.Position
{
    /// <summary>
    /// LatLong represents a set of Latitude and Longitude
    /// coordinates.
    /// </summary>
    public class LatLong
    {
        /// <summary>
        /// Latitude gets and sets the Latitude Coordinate.
        /// </summary>
        public Coordinate Latitude { get; set; }

        
        /// <summary>
        /// Longitude gets and sets the Longitude Coordinate.
        /// </summary>
        public Coordinate Longitude { get; set; }
    }
}
