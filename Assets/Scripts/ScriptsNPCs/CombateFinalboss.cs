using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerinFinal : MonoBehaviour
{
    public string nombreEscenaFinal = "Bossfight";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaFinal);
        }
    }
}
