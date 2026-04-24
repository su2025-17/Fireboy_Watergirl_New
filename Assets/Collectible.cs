using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum ColorType { Red, Blue, Yellow }
    [Header("Settings")]
    public ColorType collectibleColor;
    public int scoreValue = 1;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if Fireboy touched it
        if (other.CompareTag("Fireboy"))
        {
            // Fireboy takes Red or Yellow
            if (collectibleColor == ColorType.Red || collectibleColor == ColorType.Yellow)
            {
                Collect();
            }
        }
        // 2. Check if Watergirl touched it
        else if (other.CompareTag("Watergirl"))
        {
            // Watergirl takes Blue or Yellow
            if (collectibleColor == ColorType.Blue || collectibleColor == ColorType.Yellow)
            {
                Collect();
            }
        }
    }

    void Collect()
    {
        // Add your score logic here (e.g., ScoreManager.instance.AddScore(scoreValue);)
        Debug.Log(gameObject.name + " collected!");
        
        // Destroy the item so it disappears
        Destroy(gameObject);
    }
}