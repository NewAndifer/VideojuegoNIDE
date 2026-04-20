using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{
  
    public GameObject guidedProjectilePrefab; 
    
    public Transform firePoint;

    public float fireRate = 2f; 

    // Variable interna para llevar el control del tiempo
    private float nextFireTime = 0f;

    void Update()
    {
        // Comprobamos si el tiempo actual del juego ya superó el tiempo programado para el siguiente disparo
        if (Time.time >= nextFireTime)
        {
            Shoot();
            
            // Calculamos en qué momento del futuro podrá volver a disparar
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Validación de seguridad para evitar errores (NullReference) si olvidas asignar algo en el Inspector
        if (guidedProjectilePrefab != null && firePoint != null)
        {
            // Creamos el proyectil en la posición y con la rotación del FirePoint
            Instantiate(guidedProjectilePrefab, firePoint.position, firePoint.rotation);
        }
        else
        {
            Debug.LogWarning("Falta asignar el prefab del proyectil o el firePoint en el script del enemigo.");
        }
    }
}
