using UnityEngine;

public class IdentificadorPuerta : MonoBehaviour
{
    [Header("Identificador en base de datos")]
    [SerializeField] public int idPuerta = -1;
    [Header("Datos descargados de la base de datos")]
    [SerializeField] public bool abierta = false;
    [Header("Costo")]
    [SerializeField] public int costo = 0;

    void Start()
    {
        DescargarDatosPuerta();
    }



    private void DescargarDatosPuerta()
    {
        if (idPuerta < 0) return;

        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay datos de jugador o GameManager ff papa :(, estoy en puerta");
            return;
        }

        var arrayPuerta = GameManager.Instancia.jugadorActivo.puertasAbiertas;

        foreach (var datosPuerta in arrayPuerta)
        {
            if (datosPuerta.id_puerta == idPuerta)
            {
                abierta = datosPuerta.abierta;
                return;
            }
        
        }

        Debug.LogWarning($"Puerta ID {idPuerta} no encontrada en la DB del jugador.");

    }
}