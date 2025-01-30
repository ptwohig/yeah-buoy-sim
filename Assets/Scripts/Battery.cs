using UnityEngine;

namespace YeahBuoy.Simulation
{
    
    public class Battery : MonoBehaviour
    {
        
        [SerializeField]
        [Tooltip("The battery's useful capacity (percentage of total watt hours)")]
        [Range(0, 100)]
        public float batteryCapacity = 90f;

        [SerializeField]
        [Tooltip("The battery's watt hours.")]
        [Range(0, 40000)]
        public float batteryWattHours = 8038.4f;

        [SerializeField]
        [Tooltip("The parasitic draw from control and communication systems.")]
        public float parasiticDrawInWatts = 10.0f;

        [SerializeField] 
        [Tooltip("The maximum power draw for each motor.")]
        public float motorMaximumPower = 800f;

        public float StateOfCharge => BatteryWattHoursRemaining / UsefulCapacity;

        public float UsefulCapacity => (batteryCapacity / 100f) * batteryWattHours;

        public float BatteryWattHoursRemaining { get; }


    }
    
}