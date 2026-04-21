using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Animator animator;

    public float speed = 2.5f;

    public float stoppingDistance = 4f;

    private Transform targetPlayer;

    // Start se llama antes del primer frame de actualización
    void Start()
    {
        // Si no asignaste el Animator en el Inspector, intentamos obtenerlo del mismo objeto
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("EnemyFollow: ¡El enemigo no tiene un componente Animator assigned!");
        }

        // Reutilizamos la misma lógica de búsqueda que usaste en tu proyectil guiado
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            targetPlayer = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("EnemyFollow: No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    // Update se llama una vez por frame
    void Update()
    {
        if (targetPlayer == null) return;

        // Ahora el Update simplemente le pregunta al cerebro dónde tiene que ponerse
        transform.position = CalcularNuevaPosicion(transform.position, targetPlayer.position, speed, stoppingDistance, Time.deltaTime);
    }



    public Vector2 CalcularNuevaPosicion(Vector2 posActual, Vector2 posObjetivo, float vel, float distFrenado, float deltaTiempo)
    {
        float distanceToPlayer = Vector2.Distance(posActual, posObjetivo);

        if (distanceToPlayer > distFrenado)
        {
            return Vector2.MoveTowards(posActual, posObjetivo, vel * deltaTiempo);
        }

        return posActual;
    }
}