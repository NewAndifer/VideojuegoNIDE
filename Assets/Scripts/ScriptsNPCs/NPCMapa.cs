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

    [Header("Referencia UI")]
    [SerializeField] private LetreroDinamico miLetrero;

    void Start()
    {
        DescargarDatosNPC();
        if (miLetrero == null) miLetrero = GetComponentInChildren<LetreroDinamico>();
    }

    // --- ¡DESCOMENTAMOS ESTO! ---
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && miLetrero != null)
        {
            miLetrero.Limpiar(); 

            miLetrero.AgregarOpcion("E", "hablar");
            miLetrero.AgregarOpcion("R", "combatir");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LimpiarDatosGameManager();
            if (miLetrero != null) miLetrero.Limpiar();
        }
    }

    private void SetDatosEnGameManager()
    {
        if (GameManager.Instancia == null) return;
        GameManager.Instancia.idEnemigoActual = idNPC;
        GameManager.Instancia.operacionActual = operacion;
        GameManager.Instancia.tipoNPCActual = tipo;
        GameManager.Instancia.nombreNPCActual = nombre;
    }

    private void LimpiarDatosGameManager()
    {
        if (GameManager.Instancia != null && GameManager.Instancia.idEnemigoActual == idNPC)
        {
            GameManager.Instancia.idEnemigoActual = -1;
        }
    }

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
}