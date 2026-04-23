using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    [Header("References")]
    public Animator leverAnimator; // Drag the Sketchfab Lever here
    public GameObject floorDoor;   // Drag the Floor Door here
    
    [Header("Settings")]
    public float slideDistance = 3.0f; // How far it moves
    public float slideSpeed = 2.0f;    // How fast it moves
    
    private bool isActivated = false;
    private Vector3 targetPosition;

    void Start()
    {
        if (floorDoor != null)
        {
            // Sets the target 3 units away on the X axis
            targetPosition = floorDoor.transform.position + new Vector3(slideDistance, 0, 0);
        }
    }

    void Update()
    {
        if (isActivated && floorDoor != null)
        {
            // Moves the door smoothly to the target
            floorDoor.transform.position = Vector3.Lerp(floorDoor.transform.position, targetPosition, Time.deltaTime * slideSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            
            // Fire the Sketchfab animation trigger we made earlier
            if (leverAnimator != null)
            {
                leverAnimator.SetTrigger("Activate");
            }
            
            Debug.Log("Lever pulled! Door sliding on X.");
        }
    }
}