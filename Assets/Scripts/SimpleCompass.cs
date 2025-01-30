using UnityEngine;

namespace YeahBuoy.Simulation
{
    public class SimpleCompass : MonoBehaviour, ICompass
    {
        
        public float ChartHeading => Vector2.Angle(Vector2.up, ChartHeadingVector);
        
        public Vector2 ChartHeadingVector => new Vector2(transform.forward.x, transform.forward.z).normalized;
        
    }
    
}