using System;
using UnityEngine;

namespace YeahBuoy.Simulation
{
    
    public class SimpleMotionSensor : MonoBehaviour, IMotionSensor
    {
        
        private Vector2 _heading;
        
        public float ChartAngularVelocity { get; private set; }

        private void Start()
        {
            _heading = new Vector2(transform.forward.x, transform.forward.z).normalized;
        }
        
        private void Update()
        {
            var updated = new Vector2(transform.forward.x, transform.forward.z).normalized;
            float cross = updated.x * _heading.y - updated.y * _heading.x;
            
            ChartAngularVelocity = Time.deltaTime * Vector2.Angle(_heading, updated) * Mathf.Sign(cross);
            _heading = updated;
            
        }
        
    }
    
}