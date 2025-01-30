using System;
using NWH.DWP2.ShipController;
using UnityEngine;

namespace YeahBuoy.Simulation
{
    [Serializable]
    [RequireComponent(typeof(Navigator))]
    public class TrimPID
    {
        
        [SerializeField]
        public float Kp = 1.0f; // Proportional gain

        [SerializeField]
        public float Ki = 0.1f;
        
        [SerializeField]// Integral gain
        public float Kd = 0.5f; // Derivative gain

        private float _integral = 0f;
        
        private float _previousError; // For calculating derivative term

        private Navigator _navigator;

        private IMotionSensor _sensor;
        
        public void Initialize(Navigator navigator)
        {
            _navigator = navigator;
            _sensor = navigator.GetComponent<IMotionSensor>();
        }

        public float Calculate()
        {
            
            var desiredAngularVelocity = (_navigator.HeadingToMark / 180) * _navigator.maximumAngularVelocity;
            
            var error = desiredAngularVelocity - _sensor.ChartAngularVelocity;
            _integral += error * Time.deltaTime;

            var proportional = Kp * error;
            var integralTerm = Ki * _integral;
            var derivative = (error - _previousError) / Time.deltaTime;
            var derivativeTerm = Kd * derivative;

            var trim = proportional + integralTerm + derivativeTerm;
            trim = Mathf.Clamp(trim, -_navigator.maximumTrim, _navigator.maximumTrim);
            
            return trim;
            
        }
    }
    
}