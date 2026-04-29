using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IniciadorCombate : MonoBehaviour
{
    [SerializeField] private InputAction combatAction; // Mapeada a la tecla 'R'
    private bool isPlayerInRange;

    private void OnEnable() => combatAction.Enable();
    private void OnDisable() => combatAction.Disable();

    // Detección directa en el nieto
    private void OnTriggerEnter2D(Collider2D collision) => isPlayerInRange = collision.CompareTag("Player");
    private void OnTriggerExit2D(Collider2D collision) => isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange && combatAction.WasPressedThisFrame())
        {
            LanzarCombate();
        }
    }

    private void LanzarCombate()
    {
        // Buscamos al abuelo para sacarle la info
        NPCMapa npc = GetComponentInParent<NPCMapa>();

        // Validaciones de seguridad
        if (npc == null || npc.idNPC >= 1000 || GameManager.Instancia == null) return;

        // --- EL PUENTE DE DATOS ---
        GameManager.Instancia.idEnemigoActual = npc.idNPC;
        GameManager.Instancia.operacionActual = npc.operacion;
        GameManager.Instancia.tipoNPCActual = npc.tipo;
        GameManager.Instancia.nombreNPCActual = npc.nombre;

        GameManager.Instancia.monedasRecompensaActual = npc.monedas;

        GameManager.Instancia.ultimaEscenaMapa = SceneManager.GetActiveScene().name;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
            GameManager.Instancia.posicionRetornoMapa = player.transform.position;

        // Selección de escena
        string escena = npc.tipo.ToLower() == "boss" ? "BossCombat" : "BanditCombat";
        SceneManager.LoadScene(npc.tipo.ToLower() == "boss" ? "BossCombat" : "BanditCombat");
    }
}