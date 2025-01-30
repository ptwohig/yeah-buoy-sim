using UnityEngine;

namespace YeahBuoy.Simulation
{
    
    /// <summary>
    /// Interface to location system (such as GPS). Locates the object in the chart space.
    /// </summary>
    public interface ILocator
    {

        /// <summary>
        /// Returns the position of the object on the chart. This is the x/y coordinates, analogous to latitude and
        /// longitude.
        /// </summary>
        Vector2 ChartPosition { get; }

    }

    public static class ILocatorExtensions
    {
        
        public static float DistanceTo(this ILocator locator, Vector2 to)
        {
            return Vector2.Distance(locator.ChartPosition, to);
        }
        
        public static float DistanceTo(this ILocator locator, ILocator other)
        {
            return locator.DistanceTo(other.ChartPosition);
        }
        
    }
    
}