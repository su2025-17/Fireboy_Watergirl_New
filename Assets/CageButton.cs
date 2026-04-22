 using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CageButton : MonoBehaviour
{
    public string colorID; 
    public SlidingDoor linkedDoor;
    
    // ADD THIS LINE HERE:
    public GameObject movingPart; 

    public Vector3 pressOffset = new Vector3(0, -0.2f, 0); 
    public float animationSpeed = 5f;
    
    private bool isPressed = false;
    private Vector3 originalLocalPos;

    void Start()
    {
        // Change this line to track the child's position:
        if(movingPart != null) originalLocalPos = movingPart.transform.localPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && (other.CompareTag("Fireboy") || other.CompareTag("Watergirl")))
        {
            // Only trigger if player is close to the button's height (not jumping high above it)
            float heightDiff = Mathf.Abs(other.transform.position.y - transform.position.y);
            if (heightDiff < 1f) // Player must be within 1 unit of button height
            {
                Debug.Log("Button pressed by " + other.name);
                StartCoroutine(PressSequence());
            }
        }
    }

    IEnumerator PressSequence()
    {
        isPressed = true;
        Vector3 pressedPos = originalLocalPos + pressOffset;

        float t = 0;
        while (t < 1) {
            t += Time.deltaTime * animationSpeed;
            // Change this line to move the CHILD, not the parent:
            movingPart.transform.localPosition = Vector3.Lerp(originalLocalPos, pressedPos, t);
            yield return null;
        }

        if (linkedDoor != null && linkedDoor.colorID == this.colorID)
        {
            linkedDoor.TriggerDoor();
        }
    }
}