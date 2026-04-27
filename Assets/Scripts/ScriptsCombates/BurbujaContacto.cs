using UnityEngine;

public class BurbujaContacto : MonoBehaviour
{
    [Header("Configuración de Audio")]
    [SerializeField] public AudioClip colisionJugador;
    [SerializeField] private AudioClip colisionNPC;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PersonajeNPC"))
        {
            ControladorSonido.Instance.EjecutarSonido(colisionNPC);
            Destroy(gameObject);
        }
        else if (other.CompareTag("PersonajeJugador"))
        {
            ControladorSonido.Instance.EjecutarSonido(colisionJugador);
            Destroy(gameObject);
        }

        
    }
}