namespace YeahBuoy.Simulation
{ using UnityEngine;

public class CameraChase : MonoBehaviour
{
    
    // The object the camera will follow
    public Transform target;

    // Offset between the camera and the target
    public Vector3 offset = new Vector3(0, 5, -12);

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.LookAt(target);
        }
    }
}

}