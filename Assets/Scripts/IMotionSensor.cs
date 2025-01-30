namespace YeahBuoy.Simulation
{
    /// <summary>
    /// Accesses the motion sensors for the object. This reports information from things like gyroscopes,
    /// accelerometers, and magnetic compasses to give the device's orientation in space.
    /// </summary>
    public interface IMotionSensor
    {
        float ChartAngularVelocity { get; }
    }
    
}
