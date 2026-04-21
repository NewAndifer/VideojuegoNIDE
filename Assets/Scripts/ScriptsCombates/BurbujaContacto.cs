using UnityEngine;

public class BurbujaContacto : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Personaje"))
    {
        Destroy(gameObject);
    }
}
}