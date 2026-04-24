using UnityEngine;

public class LeverDoorController : MonoBehaviour
{
    public Animator leverAnimation; // Note: Use 'Animation', not 'Animator'
    public GameObject floorDoor;
    public float slideDistance = -15f;
    public float slideSpeed = 1.5f;
    private bool isActivated = false;
    private Vector3 targetPosition;

    void Start() {
        if (floorDoor != null) targetPosition = floorDoor.transform.position + new Vector3(slideDistance, 0, 0);
    }

    void Update() {
        if (isActivated && floorDoor != null) {
            floorDoor.transform.position = Vector3.MoveTowards(floorDoor.transform.position, targetPosition, slideSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !isActivated) {
            isActivated = true;
            // If you can't get the animator to work, just open the door!
            Debug.Log("Door opening!");
        }
    }
}