using UnityEngine;

public class CercaBloqueada : MonoBehaviour
{
    public int costo = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (GameManager.Instancia.jugadorActivo.monedas >= costo)
            {
                GameManager.Instancia.jugadorActivo.monedas -= costo;
                gameObject.SetActive(false); 
                Debug.Log("Pasaste! Monedas restantes: " + GameManager.Instancia.jugadorActivo.monedas);
            }
            else
            {
                Debug.Log("Te faltan monedas. Tienes: " + GameManager.Instancia.jugadorActivo.monedas);
            }
        }
    }
}