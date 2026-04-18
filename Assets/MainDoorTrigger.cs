using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public Transform door;
    public float openHeight = 3f;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool openDoor = false;

    void Start()
    {
        if (door == null)
        {
            door = transform; // If no door is assigned, slide the object this script is attached to.
        }

        closedPosition = door.position;
        openPosition = closedPosition + Vector3.up * openHeight;
    }

    void Update()
    {
        if (openDoor && door != null)
        {
            door.position = Vector3.MoveTowards(door.position, openPosition, openSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player has entered the trigger zone!");
            openDoor = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player has left the trigger zone!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Keep this empty unless you need repeating trigger behavior.
    }
}
