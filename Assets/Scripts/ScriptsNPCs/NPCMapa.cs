using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMapa : MonoBehaviour
{
    [Header("Identificador de Base de Datos")]
    public int idNPC = -1;
    [Header("Datos descargados NPC")]
    public string operacion = "suma";
    public string tipo = "bandido";
    public string nombre = "Desconocido";
    public bool derrotado = false;


    void Start()
    {
        DescargarDatosNPC();
    }


/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetDatosEnGameManager();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Solo limpiamos si el ID que se está borrando es el MÍO
            // Esto evita que al salir de un trigger borres los datos de otro NPC cercano
            if (GameManager.Instancia != null && GameManager.Instancia.idEnemigoActual == idNPC)
            {
                GameManager.Instancia.idEnemigoActual = -1;
                GameManager.Instancia.operacionActual = "";
                GameManager.Instancia.tipoNPCActual = "";
                GameManager.Instancia.nombreNPCActual = "";
            }
        }
    }

    */

    private void DescargarDatosNPC()
    {
        if (idNPC >= 1000 || idNPC < 0) return;

        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay datos de jugador o GameManager ff papa :(");
            return;
        }

        var arrayNPCs = GameManager.Instancia.jugadorActivo.enemigosDerrotados;


        foreach (var datosEnemigo in arrayNPCs)
        {
            if (datosEnemigo.id_npc == idNPC)
            {
                operacion = datosEnemigo.operacion;
                tipo = datosEnemigo.tipo;
                nombre = datosEnemigo.nombre;
                derrotado = datosEnemigo.derrotado;
                return;
            }
        }

        Debug.LogWarning($"NPC ID {idNPC} no encontrado en la DB del jugador.");
    }

   /*  private void SetDatosEnGameManager()
    {
        if (GameManager.Instancia == null)
        {
            Debug.LogError("No hay datos de jugador o GameManager ff papa.");
            return;
        }
        GameManager.Instancia.idEnemigoActual = idNPC;
        GameManager.Instancia.operacionActual = operacion;
        GameManager.Instancia.tipoNPCActual = tipo;
        GameManager.Instancia.nombreNPCActual = nombre;

        Debug.Log($"GameManager listo para combatir contra: {nombre} ({operacion})");
    }
    */
}