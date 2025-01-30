using UnityEngine;

namespace YeahBuoy.Simulation
{
    /// <summary>
    /// Represents the ship's compass.
    /// </summary>
    public interface ICompass
    {
        
        /// <summary>
        /// The heading relative to magnetic north.
        /// </summary>
        float ChartHeading { get; }
        
        /// <summary>
        /// The heading, as expressed as a normalized vector on the chart.
        /// </summary>
        Vector2 ChartHeadingVector { get; }
        
    }

    public static class ICompassExtensions
    {
        /// <summary>
        /// Calculates the heading using this compass.
        /// </summary>
        /// <param name="compass"></param>
        /// <param name="here"></param>
        /// <param name="there"></param>
        /// <returns></returns>
        public static float HeadingTo(this ICompass compass, ILocator here, ILocator there)
        {
            return Vector2.SignedAngle(there.ChartPosition - here.ChartPosition, compass.ChartHeadingVector);
        }
        
    }
    
}