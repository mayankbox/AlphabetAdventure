using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;
    void Update()
    {
        // Rotate the object around its local X axis at 1 degree per second
       // transform.Rotate(Vector3.right *speed* Time.deltaTime);

        // ...also rotate around the World's Y axis
        // transform.Rotate(Vector3.up *speed* Time.deltaTime, Space.World);

        transform.Rotate(0, 0, speed);
    }
}