using UnityEngine;

public class InteractionDebug : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 1. Esto se imprime siempre que algo entre al círculo
        Debug.Log("¡ALGO ENTRÓ AL TRIGGER! Objeto: " + other.name);

        // 2. Verificamos si el Tag coincide
        if (other.CompareTag("Player"))
        {
            Debug.Log("<color=green>¡ES EL JUGADOR! El tag 'Player' se detectó correctamente.</color>");
        }
        else
        {
            Debug.LogWarning("Algo entró, pero el Tag es '" + other.tag + "' y no 'Player'.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Objeto salió del trigger: " + other.name);
    }
}
