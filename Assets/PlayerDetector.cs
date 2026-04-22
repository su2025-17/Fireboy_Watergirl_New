using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public ActivationManager manager;
    public string requiredTag; // Set to "Fireboy" or "Watergirl"

    private void OnTriggerEnter(Collider other)
    {
Debug.Log("SOMETHING touched the button: " + other.gameObject.name);
        if (other.CompareTag(requiredTag)) manager.UpdatePlayerStatus(requiredTag, true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(requiredTag)) manager.UpdatePlayerStatus(requiredTag, false);
    }
}