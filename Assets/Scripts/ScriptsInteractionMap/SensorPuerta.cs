using UnityEngine;

public class SensorPuerta : MonoBehaviour
{
    private IdentificadorPuerta scriptPadre;

    void Start()
    {
        // Buscamos al "cerebro" en el objeto de arriba
        scriptPadre = GetComponentInParent<IdentificadorPuerta>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && scriptPadre != null)
        {
            scriptPadre.SetPlayerInRange(true);
            Debug.Log("Sensor: Arthur detectado");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && scriptPadre != null)
        {
            scriptPadre.SetPlayerInRange(false);
            Debug.Log("Sensor: Arthur se fue");
        }
    }
}