using UnityEngine;

public class PoisonPuddle : MonoBehaviour
{
    [Header("Settings")]
    public Transform respawnPoint; // This will show up in the Inspector now

    private void OnTriggerEnter(Collider other)
    {
        // Check if either player touched the poison
        if (other.CompareTag("Fireboy") || other.CompareTag("Watergirl"))
        {
            RespawnPlayer(other.gameObject);
        }
    }

    void RespawnPlayer(GameObject player)
    {
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // FIXED: Changed linearVelocity to velocity
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleport the player
        player.transform.position = respawnPoint.position;
        
        Debug.Log(player.name + " fell in poison! Respawning...");
    }
}