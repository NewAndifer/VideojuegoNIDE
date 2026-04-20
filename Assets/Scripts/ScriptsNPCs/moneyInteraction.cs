using UnityEngine;

public class InteraccionAldeano : MonoBehaviour
{
    public int recompensaMonedas = 5;
    public GameObject textoVisual; // El texto que dice "¡Hola!" o "Interactuar"
    private bool yaDioMonedas = false; // Para que no te dé dinero infinito

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !yaDioMonedas)
        {
            if(textoVisual != null) textoVisual.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(textoVisual != null) textoVisual.SetActive(false);
            
            // Si quieres que las monedas se den al alejarse (terminar la interacción)
            FinalizarInteraccion();
        }
    }

    void FinalizarInteraccion()
    {
        if (!yaDioMonedas)
        {
            EstadoJuego.monedas += recompensaMonedas;
            yaDioMonedas = true; // Marcamos que ya cumplió
            Debug.Log("¡Aldeano te dio 5 monedas! Total: " + EstadoJuego.monedas);
            
            // Opcional: Desactivar el texto visual para siempre
            if(textoVisual != null) textoVisual.SetActive(false);
        }
    }
}