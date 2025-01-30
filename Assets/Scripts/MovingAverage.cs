using System;

namespace YeahBuoy.Simulation
{
    public class MovingAverage
    {

        private int _index;

        private readonly float[] _values;
        
        public float Value { get; private set; }

        public MovingAverage(int size, float value = float.NaN)
        {
            Value = value;
            _values = new float[size];
            Array.Fill(_values, value);
        }

        public void Update(float value)
        {

            if (value == float.NaN)
            {
                throw new ArgumentException("Value cannot be NaN.");
            }
            
            _values[_index] = value;
            _index = (_index + 1) % _values.Length;

            float sum = 0;

            foreach (var t in _values)
            {
                if (!float.IsNaN(t))
                {
                    sum += t;
                }
            }
            
            Value = sum / _values.Length;
            
        }
        
    }
}