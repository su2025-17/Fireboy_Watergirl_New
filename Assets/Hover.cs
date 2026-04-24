using UnityEngine;

public class HoverAnimation : MonoBehaviour
{
    [Header("Settings")]
    public float rotationSpeed = 50f;
    public float floatAmplitude = 0.5f; // How high it goes
    public float floatFrequency = 1f;   // How fast it bobs

    private Vector3 startPos;

    void Start()
    {
        // Record the starting position
        startPos = transform.position;
    }

    void Update()
    {
        // 1. Rotation (Spinning)
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // 2. Floating (Sine Wave)
        Vector3 tempPos = startPos;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPos;
    }
}