using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class IniciadorCombate : MonoBehaviour
{
    [SerializeField] private InputAction combatAction; // Mapeada a la tecla 'R'
    private bool isPlayerInRange;

    private void OnEnable() => combatAction.Enable();
    private void OnDisable() => combatAction.Disable();

    private void OnTriggerEnter2D(Collider2D collision) => isPlayerInRange = collision.CompareTag("Player");
    private void OnTriggerExit2D(Collider2D collision) => isPlayerInRange = false;

    void Update()
    {
        // Solo puede iniciar combate si NO está hablando ya
        if (isPlayerInRange && combatAction.WasPressedThisFrame())
        {
            LanzarCombate();
        }
    }

    private void LanzarCombate()
    {
        NPCMapa npc = GetComponentInParent<NPCMapa>();
        if (npc == null || npc.idNPC >= 1000 || GameManager.Instancia == null) return;

        if (npc != null)
        {
           
            GameManager.Instancia.idEnemigoActual = npc.idNPC;
            GameManager.Instancia.operacionActual = npc.operacion; // ¡Esto faltaba!
            GameManager.Instancia.tipoNPCActual = npc.tipo;       // ¡Esto también!
            GameManager.Instancia.nombreNPCActual = npc.nombre;

            GameManager.Instancia.posicionRetornoMapa = GameObject.FindWithTag("Player").transform.position;

            string escena = npc.tipo.ToLower() == "boss" ? "BossCombat" : "BanditCombat";
            SceneManager.LoadScene(escena);
        }
    }
}