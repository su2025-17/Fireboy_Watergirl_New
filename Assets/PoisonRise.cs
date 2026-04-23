using UnityEngine;

public class PoisonRise : MonoBehaviour
{
    [Header("Rise Settings")]
    public float riseSpeed = 0.1f; // Adjust this for "slow but visible"
    public bool isRising = true;

    void Update()
    {
        if (isRising)
        {
            // Moves the cube upward along the Y axis
            transform.Translate(Vector3.up * riseSpeed * Time.deltaTime, Space.World);
        }
    }

    // This function will be called by the lever
    public void StopPoison()
    {
        isRising = false;
        Debug.Log("Poison has stopped rising!");
    }
}