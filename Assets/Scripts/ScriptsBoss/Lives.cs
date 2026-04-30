using UnityEngine;
using UnityEngine.SceneManagement;

public class Lives : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;

        if (BossUIManager.Instance != null)
        {
            BossUIManager.Instance.UpdateHearts(currentLives);
        }
    }

    public void TakeDamage(int amount)
    {
        currentLives -= amount;
        if (currentLives < 0) currentLives = 0;

        if (BossUIManager.Instance != null)
        {
            BossUIManager.Instance.UpdateHearts(currentLives);
        }

        if (currentLives <= 0)
        {
            if(GameManager.Instancia != null)
            SceneManager.LoadScene(GameManager.Instancia.ultimaEscenaMapa);
        }
    }
}