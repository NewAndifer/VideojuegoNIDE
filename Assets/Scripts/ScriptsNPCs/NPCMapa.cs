using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCMapa : MonoBehaviour
{
    [Header("Identificador de Base de Datos")]
    public int idNPC; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConfigurarYEntrarACombate();
        }
    }

    private void ConfigurarYEntrarACombate()
    {
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay datos de jugador o GameManager.");
            return;
        }

        // Buscamos los datos del NPC en el array que obtuviste del login
        var arrayNPCs = GameManager.Instancia.jugadorActivo.enemigosDerrotados;
        
        bool encontrado = false;
        string tipoEncontrado = "";

        foreach (var datosEnemigo in arrayNPCs)
        {
            if (datosEnemigo.id_npc == idNPC)
            {
                GameManager.Instancia.idEnemigoActual = datosEnemigo.id_npc;
                GameManager.Instancia.operacionActual = datosEnemigo.operacion;
                GameManager.Instancia.tipoNPCActual = datosEnemigo.tipo; 
                
                tipoEncontrado = datosEnemigo.tipo;
                Debug.Log($"NPC Encontrado: {datosEnemigo.nombre}. Iniciando combate de {datosEnemigo.operacion}");
                encontrado = true;
                break;
            }
        }

        if (encontrado )
        {
           string tipoNormalizado = tipoEncontrado.Trim().ToLower();

            if (tipoNormalizado == "boss")
            {
                Debug.Log("Entrando a combate de BOSS");
                SceneManager.LoadScene("BossCombat");
            }
            else
            {
                Debug.Log("Entrando a combate de Bandido");
                SceneManager.LoadScene("BanditCombat");
            }
        }
        else
        {
            Debug.LogWarning($"El ID {idNPC} no existe en los datos del jugador actual.");
        }
    }
}