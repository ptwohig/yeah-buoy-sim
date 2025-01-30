using System;
using System.Collections;
using NWH.DWP2.ShipController;
using UnityEngine;
using UnityEngine.Serialization;

namespace YeahBuoy.Simulation
{
    public class Navigator : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("The buoy's target transform.")]
        public Transform markTarget;

        [SerializeField]
        [Tooltip("The distance from the mark target which will activate the buoy's thrusters.")]
        [Range(0, 50)]
        public float minimumDistance = 2f;
        
        [SerializeField]
        [Tooltip("The distance at which the buoy will enter approach mode and throttle down the thrusters for holding position.")]
        [Range(0, 1000)]
        public float maximumDistance = 10f;
        
        [SerializeField]
        [Tooltip("The maximum trim factor for the motors.")]
        [Range(0, 1)]
        public float maximumTrim = 1;

        [SerializeField]
        [Tooltip("The maximum throttle for the motors.")]
        [Range(0, 1)]
        public float maximumThrottle = 1;

        [SerializeField]
        [Tooltip("The time it takes to throttle up the motors when starting cold.")]
        public float throttleUpTime = 2;
        
        [SerializeField]
        [Tooltip("The maximum angular velocity of the buoy when controlling the trim factor.")]
        public float maximumAngularVelocity = .01f;

        [SerializeField]
        [Tooltip("The amount of time the buoy will linger around the mark before attempting to ")]
        public float markLingerTime = 1.0f;
        
        [SerializeField] public TrimPID trimPID = new TrimPID();
        
        public float Trim { get; private set; }

        public float Throttle { get; private set; }

        public float HeadingToMark { get; private set; }

        public float DistanceToMark { get; private set; }

        public void Start()
        {
            var controller = GetComponent<AdvancedShipController>();
            trimPID.Initialize(this);
            controller.input.autoSetInput = false;
            StartCoroutine(Navigation(controller));
            StartCoroutine(CalculateHeadingTrimAndThrottle());
        }

        private IEnumerator Navigation(AdvancedShipController controller)
        {
            do
            {
                yield return ThrottleUp(controller);
                yield return NavigateToMark(controller);
                yield return WaitToLeaveMark(controller);
            } while (true);
        }

        private IEnumerator ThrottleUp(AdvancedShipController controller)
        {
            Debug.Log("ThrottleUp");
            controller.input.Throttle = maximumThrottle;
            controller.input.Throttle2 = maximumThrottle;
            yield return new WaitForSeconds(throttleUpTime);
        }

        private IEnumerator NavigateToMark(AdvancedShipController controller)
        {
            
            Debug.Log("Navigating Mark.");

            while (DistanceToMark > minimumDistance)
            {
                
                float port, starboard;

                if (Trim > 0)
                {
                    port = Throttle;
                    starboard = Throttle * (1f - Mathf.Abs(Trim));
                }
                else
                {
                    port = Throttle * (1f - Mathf.Abs(Trim));
                    starboard = Throttle;
                }

                controller.input.Throttle = port;
                controller.input.Throttle2 = starboard;

                yield return null;
                
            }

        }
        
        private IEnumerator WaitToLeaveMark(AdvancedShipController controller)
        {

            Debug.Log("Arrived at mark.");

            controller.input.Throttle = 0;
            controller.input.Throttle2 = 0;

            while (DistanceToMark < maximumDistance)
            {
                yield return new WaitForSeconds(throttleUpTime);
            }
            
        }

        private IEnumerator CalculateHeadingTrimAndThrottle()
        {

            var mark = markTarget.GetComponent<ILocator>();
            var locator = GetComponent<ILocator>();
            var compass = GetComponent<ICompass>();

            do
            {

                HeadingToMark = compass.HeadingTo(locator, mark);
                DistanceToMark = locator.DistanceTo(mark);

                // Calculates the distance and tapers off distance as the buoy approaches the mark.
                
                if (DistanceToMark < minimumDistance)
                {
                    Throttle = 0;
                }
                else
                {
                    Throttle = Mathf.Min(1.0f, DistanceToMark / maximumDistance) * maximumThrottle;
                }

                // Calculates the desired angular velocity and tapers off as the buoy's course lines up with the mark.

                Trim = trimPID.Calculate();

                yield return null;
                
            } while (true);
            
        }

        public void OnDrawGizmos()
        {

            var position = transform.position;

            var compass = GetComponent<ICompass>();
            var chartHeading = compass.ChartHeadingVector;
            Gizmos.color = Color.magenta;
            Gizmos.DrawRay(position, new Vector3(chartHeading.x, 0, chartHeading.y) * 2.5f);
            
            Vector3 direction;
                
            direction = Quaternion.AngleAxis(HeadingToMark, Vector3.up) * transform.forward;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(position, direction * DistanceToMark);
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(markTarget.position, minimumDistance);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(markTarget.position, maximumDistance);
            
        }
        
    }

}
