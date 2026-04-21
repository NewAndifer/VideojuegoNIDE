using UnityEngine;

public class InteraccionAldeano : MonoBehaviour
{
    public int recompensaMonedas = 5;
    private GameObject textoVisual;
    private bool yaDioMonedas = false;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !yaDioMonedas)
        {
            yaDioMonedas = true;
            DarMonedas();
        }
    }

    private void DarMonedas()
    {
        if (GameManager.Instancia != null && GameManager.Instancia.jugadorActivo != null)
        {
             GameManager.Instancia.jugadorActivo.monedas += recompensaMonedas;
        }
    }

}