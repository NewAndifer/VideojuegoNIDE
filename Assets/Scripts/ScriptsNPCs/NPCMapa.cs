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
            ConfigurarNPC(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instancia != null && GameManager.Instancia.jugadorActivo != null)
            {
                GameManager.Instancia.idEnemigoActual = -1;
                GameManager.Instancia.operacionActual = "";
                GameManager.Instancia.tipoNPCActual = "";
                GameManager.Instancia.nombreNPCActual = "";
            }
        }

    }

    private void ConfigurarNPC(GameObject jugador)
    {
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay datos de jugador o GameManager ff papa.");
            return;
        }

        //GameManager.Instancia.posicionRetornoMapa = jugador.transform.position + new Vector3(0, -1.5f, 0);

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
                GameManager.Instancia.nombreNPCActual = datosEnemigo.nombre;

                tipoEncontrado = datosEnemigo.tipo;
                Debug.Log($"NPC Encontrado: {datosEnemigo.nombre}. Iniciando combate de {datosEnemigo.operacion}");
                encontrado = true;
                break;
            }
        }

        if (encontrado)
        {
            /*
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
            */
        }
        else
        {
            Debug.LogWarning($"El ID {idNPC} no existe en los datos del jugador actual.");
        }
    }
}