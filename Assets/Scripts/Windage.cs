using Crest;
using UnityEngine;

namespace YeahBuoy.Simulation
{
    public class Windage : MonoBehaviour
    {
        
        [SerializeField]
        private ShapeWaves shape;

        [SerializeField]
        private float factor = 0.0001f;
        
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Update()
        {
            var wind = shape.PrimaryWaveDirection * (shape.WindSpeed * factor); 
            _rigidbody.AddForce(wind, ForceMode.Force);
        }
     
        
    }
}