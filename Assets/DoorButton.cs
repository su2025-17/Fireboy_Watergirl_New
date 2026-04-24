using UnityEngine;

public class GroupButton : MonoBehaviour
{
    [Header("Links")]
    public SlidingDoor targetDoor; 
    public GameObject visualButtonPart; // Drag the actual button mesh here

    [Header("Settings")]
    public float sinkAmount = 0.15f; 
    public float sinkSpeed = 5f;

    private bool pressed = false;
    private Vector3 pressedLocalPos;

    void Start()
    {
        if (visualButtonPart != null)
        {
            // We use LocalPosition so it moves relative to the group
            pressedLocalPos = visualButtonPart.transform.localPosition - new Vector3(0, sinkAmount, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!pressed)
        {
            pressed = true;
            if (targetDoor != null) targetDoor.TriggerDoor();
            Debug.Log("Group Button activated!");
        }
    }

    void Update()
    {
        if (pressed && visualButtonPart != null)
        {
            // Smoothly move only the visual part, not the whole group
            visualButtonPart.transform.localPosition = Vector3.Lerp(
                visualButtonPart.transform.localPosition, 
                pressedLocalPos, 
                Time.deltaTime * sinkSpeed
            );
        }
    }
}