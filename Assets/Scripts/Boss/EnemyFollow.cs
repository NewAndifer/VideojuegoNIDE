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
        // Medida de seguridad: si el jugador fue destruido o no existe, salimos del Update
        if (targetPlayer == null) return;

        // Calculamos la distancia exacta entre el enemigo y el jugador
        
        float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);

        // Si la distancia es mayor a nuestro límite de frenado, nos acercamos
        if (distanceToPlayer > stoppingDistance)


        {
            // MoveTowards toma 3 valores: Posición actual, Posición objetivo, y la velocidad máxima por frame
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
        }
        
    }
}