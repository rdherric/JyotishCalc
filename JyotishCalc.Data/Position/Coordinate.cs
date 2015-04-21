using System;

namespace JyotishCalc.Data.Position
{
    /// <summary>
    /// Coordinate represents a Latitude or Longitude coordinate
    /// on the Earth in Degrees, Minutes, and Seconds.
    /// </summary>
    public struct Coordinate
    {
        /// <summary>
        /// Degrees gets and sets the Degrees of the Coordinate.
        /// </summary>
        public int Degrees { get; set; }


        /// <summary>
        /// Minutes gets and sets the Minutes of the Coordinate.
        /// </summary>
        public int Minutes { get; set; }


        /// <summary>
        /// Seconds gets and sets the Seconds of the Coordinate.
        /// </summary>
        public int Seconds { get; set; }
    }
}
