using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinal : MonoBehaviour
{
    [Header("Configuración de Escena")]
    public string nombreEscenaFinal = "BanditCombat";

    [Header("Configuración del Combate")]
    public int idEnemigoBaseDeDatos; // El ID que definieron en su DB (ej. 1, 2, 3)
    public string operacionParaEsteCombate; // "suma", "resta", "multiplicacion" o "division"

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 1. Antes de irnos, guardamos la configuración en el GameManager
            if (GameManager.Instancia != null)
            {
                GameManager.Instancia.idEnemigoActual = idEnemigoBaseDeDatos;
                GameManager.Instancia.operacionActual = operacionParaEsteCombate;
                
                Debug.Log($"Preparando combate contra NPC {idEnemigoBaseDeDatos}. Operación: {operacionParaEsteCombate}");
            }
            else
            {
                Debug.LogError("No se encontró el GameManager en la escena. ¡Asegúrate de que exista!");
            }

            // 2. Ahora sí, cargamos la escena de las preguntas
            SceneManager.LoadScene(nombreEscenaFinal);
        }
    }
}