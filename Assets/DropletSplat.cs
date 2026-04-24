using UnityEngine;

public class DropletSplat : MonoBehaviour
{
    private bool hasSplat = false;

    void OnCollisionEnter(Collision collision)
    {
        // Only splat once and only if it hits the floor
        if (!hasSplat && collision.gameObject.CompareTag("Floor"))
        {
            hasSplat = true;
            Splat();
        }
    }

    void Splat()
    {
        // Disable physics so it doesn't bounce
        GetComponent<Rigidbody>().isKinematic = true;
        
        //Disable the collider so it stops rolling
        GetComponent<Collider>().enabled = false;

        // Flatten the scale: Squish Y, stretch X and Z
        transform.localScale = new Vector3(2.0f, 0.1f, 2.0f);
        
        // Optional: Move it slightly up so it's not clipping into the floor
        transform.position += new Vector3(0, 0.05f, 0);

        // Destroy after 2 seconds so they don't clutter the scene
        Destroy(gameObject, 2f);
    }
}