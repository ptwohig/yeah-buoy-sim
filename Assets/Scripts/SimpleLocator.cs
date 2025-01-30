using UnityEngine;


namespace YeahBuoy.Simulation
{
    public class SimpleLocator : MonoBehaviour, ILocator
    {

        public Vector2 ChartPosition => new(transform.position.x, transform.position.z);

    }
}