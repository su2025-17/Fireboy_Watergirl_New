using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform exitPoint; // Drag "teleportation_exit" here in Inspector
    public string playerTag = "Watergirl"; // Change to "Fireboy" for his portal
    public AudioClip teleportSound;
    public AudioClip voiceoverClip;
    public float voiceoverDelay = 1f; // Delay before voiceover plays
    public float voiceoverSpeed = 1f; // Speed of voiceover (1 = normal)

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the correct player
        if (other.CompareTag(playerTag))
        {
            // Move the player to the exit point's position
            // We add a small offset (1f) so they don't get stuck in the floor
            other.transform.position = exitPoint.position + Vector3.up * 1f;
            
            // Play sound effect immediately
            if (teleportSound != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
            
            // Play voiceover after delay
            if (voiceoverClip != null)
            {
                StartCoroutine(PlayVoiceoverDelayed());
            }
            
            // Optional: If you have a particle effect, play it here!
            Debug.Log(playerTag + " teleported!");
        }
    }

    IEnumerator PlayVoiceoverDelayed()
    {
        yield return new WaitForSeconds(voiceoverDelay);
        
        // Set speed for voiceover
        float originalSpeed = audioSource.pitch;
        audioSource.pitch = voiceoverSpeed;
        
        audioSource.PlayOneShot(voiceoverClip);
        
        // Reset speed after playing
        yield return new WaitForSeconds(voiceoverClip.length / voiceoverSpeed);
        audioSource.pitch = originalSpeed;
    }
}
