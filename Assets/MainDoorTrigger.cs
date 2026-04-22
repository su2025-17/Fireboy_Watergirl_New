using UnityEngine;

public class TriggerAction : MonoBehaviour
{
    public Transform door;
<<<<<<< Updated upstream
    public float openHeight = 3f;
    public float openSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool openDoor = false;
=======
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioClip openSound;

    private Quaternion closedRotation;
    private Quaternion openRotation;
    private bool openDoor = false;
    private AudioSource audioSource;
>>>>>>> Stashed changes

    void Start()
    {
        if (door == null)
        {
<<<<<<< Updated upstream
            door = transform; // If no door is assigned, slide the object this script is attached to.
        }

        closedPosition = door.position;
        openPosition = closedPosition + Vector3.up * openHeight;
=======
            door = transform; // If no door is assigned, rotate the object this script is attached to.
        }

        closedRotation = door.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, openAngle, 0);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
>>>>>>> Stashed changes
    }

    void Update()
    {
        if (openDoor && door != null)
        {
<<<<<<< Updated upstream
            door.position = Vector3.MoveTowards(door.position, openPosition, openSpeed * Time.deltaTime);
=======
            door.rotation = Quaternion.RotateTowards(door.rotation, openRotation, openSpeed * Time.deltaTime);
>>>>>>> Stashed changes
        }
    }

    private void OnTriggerEnter(Collider other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player has entered the trigger zone!");
            openDoor = true;
=======
        if (other.CompareTag("Fireboy"))
        {
            Debug.Log("Fireboy has entered the trigger zone!");
            openDoor = true;
            if (openSound != null)
            {
                audioSource.PlayOneShot(openSound);
            }
>>>>>>> Stashed changes
        }
    }

    private void OnTriggerExit(Collider other)
    {
<<<<<<< Updated upstream
        if (other.CompareTag("Player"))
        {
            Debug.Log("The player has left the trigger zone!");
=======
        if (other.CompareTag("Fireboy"))
        {
            Debug.Log("Fireboy has left the trigger zone!");
>>>>>>> Stashed changes
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // Keep this empty unless you need repeating trigger behavior.
    }
}
