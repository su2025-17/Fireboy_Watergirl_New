using UnityEngine;

public class SweeperRotation : MonoBehaviour
{
    [Range(0, 500)]
    public float speed = 150f;
    public float knockbackForce = 15f; // Strength of the hit

    void Update()
    {
        // Your original rotation logic
        transform.Rotate(0, -speed * Time.deltaTime, 0, Space.World);
    }

    // This function detects the physical hit
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the thing we hit is a player
        if (collision.gameObject.CompareTag("Fireboy") || collision.gameObject.CompareTag("Watergirl"))
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();

            if (playerRb != null)
            {
                // Calculate direction to push the player (away from the sweeper center)
                Vector3 pushDir = collision.transform.position - transform.position;
                pushDir.y = 0; // Keep the push horizontal

                // Apply the force
                playerRb.AddForce(pushDir.normalized * knockbackForce, ForceMode.Impulse);
                
                Debug.Log("Sweeper hit: " + collision.gameObject.name);
            }
        }
    }
}