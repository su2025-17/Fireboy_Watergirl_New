using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 3f, 0); // How high it slides
    public float speed = 2f;
    public AudioClip openSound;
    
    private Vector3 closedPos;
    private Vector3 openPos;
    private bool shouldOpen = false;
    private AudioSource audioSource;

    void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + openOffset;
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    // This is the function the button will call
    public void TriggerDoor()
    {
        shouldOpen = true;
        if (openSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(openSound);
        }
    }

    void Update()
    {
        if (shouldOpen)
        {
            transform.position = Vector3.Lerp(transform.position, openPos, Time.deltaTime * speed);
        }
    }
}