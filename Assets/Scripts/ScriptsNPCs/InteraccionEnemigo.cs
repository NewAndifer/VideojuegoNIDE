using UnityEngine;
using UnityEngine.SceneManagement;

public class InteraccionEnemigo : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaFinal = "BanditCombat";

    [Header("Configuración del Combate")]
    public int idEnemigoBaseDeDatos;
    public string operacionParaEsteCombate;
    public string tipo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (GameManager.Instancia != null)
            {
                GameManager.Instancia.idEnemigoActual = idEnemigoBaseDeDatos;
                GameManager.Instancia.operacionActual = operacionParaEsteCombate;
                GameManager.Instancia.tipoNPCActual = this.tipo;

                Debug.Log($"NPC tipo: {tipo} detectado.");
                Debug.Log($"Preparando combate contra NPC {idEnemigoBaseDeDatos}. Operación: {operacionParaEsteCombate}");
            }
            else
            {
                Debug.LogError("No se encontró el GameManager en la escena. ¡Asegúrate de que exista!");
            }

            string tipoNormalizado = tipo.Trim().ToLower();

            if (tipoNormalizado == "boss")
            {
                //SceneManager.LoadScene("BossCombat");
            }
            else
            {
                //SceneManager.LoadScene(nombreEscenaFinal);
            }
        }
    }
}
