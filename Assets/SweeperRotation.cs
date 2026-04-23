using UnityEngine;

public class SweeperRotation : MonoBehaviour
{
    [Range(0, 500)]
    public float speed = 150f;

    void Update()
    {
        // 0 on X, 'speed' on Y, 0 on Z. 
        // Space.World ensures the rotation stays flat regardless of the cylinder's orientation.
        transform.Rotate(0, speed * Time.deltaTime, 0, Space.World);
    }
}