using UnityEngine;

public class Lives : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
        
        // Optional: Ensure the UI starts with 3 lives visually
        if (BossUIManager.Instance != null)
        {
            BossUIManager.Instance.UpdateHearts(currentLives);
        }
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        if (currentLives < 0) currentLives = 0;

        // Pass the new number of lives to our UI Toolkit script
        if (BossUIManager.Instance != null)
        {
            BossUIManager.Instance.UpdateHearts(currentLives);
        }

        if (currentLives <= 0)
        {
            Debug.Log("Player Defeated");
            // Handle Game Over logic here
        }
    }
}