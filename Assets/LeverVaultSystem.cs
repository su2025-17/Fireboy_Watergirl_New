using UnityEngine;

public class LeverVaultSystem : MonoBehaviour
{
    [Header("Lever Settings")]
    public Animator leverAnimator;
    public string animationName = "LeverPull";
    public float stopTime = 1.7f; // Stops at 1.7 seconds

    [Header("Door Settings")]
    public GameObject vaultDoor;
    public Vector3 slideDirection = new Vector3(2f, 0, 0); // Distance and direction it slides
    public float slideSpeed = 2f;

    private bool isTriggered = false;
    private bool doorOpening = false;
    private Vector3 targetDoorPosition;

    void Start()
    {
        if (vaultDoor != null)
            targetDoorPosition = vaultDoor.transform.position + slideDirection;
    }

    void OnTriggerEnter(Collider other)
    {
        // Change "Player" to the tag your character uses
        if (other.CompareTag("Player") && !isTriggered)
        {
            ActivateMechanism();
        }
    }

    void ActivateMechanism()
    {
        isTriggered = true;
        
        // Start the animation
        leverAnimator.Play(animationName);
        
        // Tell the script to stop the animation after X seconds
        Invoke("StopLeverAnimation", stopTime);

        // Start opening the door
        doorOpening = true;
    }

    void StopLeverAnimation()
    {
        // Sets the animation speed to 0 to "freeze" it
        leverAnimator.speed = 0;
    }

    void Update()
    {
        if (doorOpening && vaultDoor != null)
        {
            // Smoothly slide the door to the target position
            vaultDoor.transform.position = Vector3.MoveTowards(
                vaultDoor.transform.position, 
                targetDoorPosition, 
                slideSpeed * Time.deltaTime
            );

            // Stop moving once it's close enough
            if (Vector3.Distance(vaultDoor.transform.position, targetDoorPosition) < 0.01f)
            {
                doorOpening = false;
            }
        }
    }
}