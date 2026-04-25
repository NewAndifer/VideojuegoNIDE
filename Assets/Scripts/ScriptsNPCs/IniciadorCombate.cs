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
        NPCMapa mapa = GetComponentInParent<NPCMapa>();
        if (mapa == null || mapa.idNPC >= 1000) return;

        var datos = System.Array.Find(GameManager.Instancia.jugadorActivo.enemigosDerrotados, e => e.id_npc == mapa.idNPC);

        if (datos != null)
        {
            // --- AQUÍ ESTÁ LA CLAVE ---
            // Sincronizamos TODO el perfil del enemigo en el GameManager
            GameManager.Instancia.idEnemigoActual = datos.id_npc;
            GameManager.Instancia.operacionActual = datos.operacion; // ¡Esto faltaba!
            GameManager.Instancia.tipoNPCActual = datos.tipo;       // ¡Esto también!
            GameManager.Instancia.nombreNPCActual = datos.nombre;

            GameManager.Instancia.posicionRetornoMapa = GameObject.FindWithTag("Player").transform.position;

            string escena = datos.tipo.ToLower() == "boss" ? "BossCombat" : "BanditCombat";
            SceneManager.LoadScene(escena);
        }
    }
}