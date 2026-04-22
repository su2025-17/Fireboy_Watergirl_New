using UnityEngine;
using System.Collections;

public class ActivationManager : MonoBehaviour
{
    [Header("Door Settings")]
    public Transform doorHinge; 
    public float openAngle = -90f;

    [Header("Detection Settings")]
    public Light[] patchLights;    
    public AudioSource risingSFX;
    
    [Header("Visual Growth & Movement")]
    public Transform[] glowDiscs; 
    public MeshRenderer[] glowRenderers; 
    
    private bool fireboyStanding = false;
    private bool watergirlStanding = false;
    private float fireboyStandingTime = 0f;
    private float watergirlStandingTime = 0f;
    private float requiredStandingTime = 3f;
    private bool isOpeningDoor = false;

    // Position & Scale Constants
    private float startPosX = 0.065f;
    private float targetPosX = 0.081f;
    private Vector3 smallScale = new Vector3(5f, 5f, 2.36f); 
    private Vector3 fullScale = new Vector3(29.6f, 29.6f, 2.36f);

    // --- UPDATED DEBUG METHOD ---
    public void UpdatePlayerStatus(string tag, bool isStanding)
    {
        Debug.Log($"UpdatePlayerStatus called! Tag received: '{tag}', Is Standing: {isStanding}");

        if (tag == "Fireboy") 
        {
            fireboyStanding = isStanding;
            Debug.Log("Fireboy's standing status is now: " + fireboyStanding);
        }
        else if (tag == "Watergirl") 
        {
            watergirlStanding = isStanding;
            Debug.Log("Watergirl's standing status is now: " + watergirlStanding);
        }
        else
        {
            Debug.LogWarning($"Unrecognized tag: '{tag}'. Make sure it is exactly 'Fireboy' or 'Watergirl' with no extra spaces!");
        }
    }

    void Update()
    {
        if (isOpeningDoor) return;

        // 1. Update Timers
        fireboyStandingTime = fireboyStanding ? Mathf.Min(fireboyStandingTime + Time.deltaTime, requiredStandingTime) : 0f;
        watergirlStandingTime = watergirlStanding ? Mathf.Min(watergirlStandingTime + Time.deltaTime, requiredStandingTime) : 0f;

        // 2. Audio Logic: Play only if BOTH are standing and we haven't finished
        if (fireboyStanding && watergirlStanding && fireboyStandingTime < requiredStandingTime)
        {
            if (!risingSFX.isPlaying) risingSFX.Play();
        }
        else if (!fireboyStanding || !watergirlStanding)
        {
            if (risingSFX.isPlaying) risingSFX.Stop();
        }

        // 3. Update Visuals
        UpdateGlowProgress(0, fireboyStandingTime); // Fireboy
        UpdateGlowProgress(1, watergirlStandingTime); // Watergirl

        // 4. Trigger Door
        if (fireboyStandingTime >= requiredStandingTime && watergirlStandingTime >= requiredStandingTime)
        {
            isOpeningDoor = true;
            if (risingSFX.isPlaying) risingSFX.Stop();
            StartCoroutine(OpenDoorRoutine());
            Debug.Log("DOOR IS OPENING!"); // Added a little debug here too!
        }
    }

    void UpdateGlowProgress(int index, float standingTime)
    {
        if (index >= glowDiscs.Length) return;
        
        float progress = standingTime / requiredStandingTime;

        // Movement & Scale
        if (glowDiscs[index] != null)
        {
            float currentX = Mathf.Lerp(startPosX, targetPosX, progress);
            glowDiscs[index].localPosition = new Vector3(currentX, glowDiscs[index].localPosition.y, glowDiscs[index].localPosition.z);
            glowDiscs[index].localScale = Vector3.Lerp(smallScale, fullScale, progress);
        }

        // Light
        if (index < patchLights.Length && patchLights[index] != null)
        {
            patchLights[index].intensity = Mathf.Lerp(0.2f, 5f, progress);
        }

        // Color/Emission
        if (index < glowRenderers.Length && glowRenderers[index] != null)
        {
            Color baseColor = glowRenderers[index].material.GetColor("_Color"); 
            Color emissionColor = baseColor * Mathf.LinearToGammaSpace(progress * 4f);
            glowRenderers[index].material.SetColor("_EmissionColor", emissionColor);
            DynamicGI.SetEmissive(glowRenderers[index], emissionColor);
        }
    }

    IEnumerator OpenDoorRoutine()
    {
        Quaternion targetRotation = Quaternion.Euler(0, openAngle, 0);
        Quaternion startRotation = doorHinge.localRotation;
        float elapsed = 0;
        while (elapsed < 1f) 
        {
            elapsed += Time.deltaTime;
            doorHinge.localRotation = Quaternion.Slerp(startRotation, targetRotation, elapsed);
            yield return null;
        }
    }
}