using UnityEngine;

public class RecuperarPosicion : MonoBehaviour
{
    void Start()
    {
        // Si el GameManager tiene una posición guardada (diferente de cero), movemos al jugador ahí
        if (GameManager.Instancia != null && GameManager.Instancia.posicionRetornoMapa != Vector3.zero)
        {
            transform.position = GameManager.Instancia.posicionRetornoMapa;
            
            // Opcional: Limpiar la posición para que no se use de nuevo si mueres o reinicias
            // GameManager.Instancia.posicionRetornoMapa = Vector3.zero;
        }
    }
}