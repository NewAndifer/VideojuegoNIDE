using UnityEngine;

public class EnemyOrientation : MonoBehaviour
{
    private Transform targetPlayer;
    private bool mirandoDerecha = true;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) targetPlayer = playerObj.transform;
    }

    void Update()
    {
        if (targetPlayer == null) return;

        // Comprobamos si el jugador cruzó al otro lado
        if (targetPlayer.position.x > transform.position.x && mirandoDerecha)
        {
            Flip();
        }
        else if (targetPlayer.position.x < transform.position.x && !mirandoDerecha)
        {
            Flip();
        }
    }

    void Flip()
    {
        mirandoDerecha = !mirandoDerecha;

        // Multiplicamos la escala en X por -1 para crear el efecto espejo
        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}