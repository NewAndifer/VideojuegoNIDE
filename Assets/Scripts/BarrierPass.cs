using UnityEngine;

public class CercaBloqueada : MonoBehaviour
{
    public int costo = 5;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (EstadoJuego.monedas >= costo)
            {
                EstadoJuego.monedas -= costo;
                gameObject.SetActive(false); 
                Debug.Log("Pasaste! Monedas restantes: " + EstadoJuego.monedas);
            }
            else
            {
                Debug.Log("Te faltan monedas. Tienes: " + EstadoJuego.monedas);
            }
        }
    }
}