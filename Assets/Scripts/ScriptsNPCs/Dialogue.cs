using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text npcNameText;

    [Header("Configuración de Textos")]
    [SerializeField, TextArea(4, 6)] private string[] lineasNormales;
    [SerializeField, TextArea(4, 6)] private string[] lineasDerrotado;

    private string[] lineasActivas;

    [Header("Input")]
    [SerializeField] private InputAction interactAction;

    [Header("Audio del Diálogo")]
    [SerializeField] private AudioClip audioAmbienteDialogo;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex = 0;
    private float typingTime = 0.05f;
    private string lineaActualProcesada;

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    void Update()
    {
        if (isPlayerInRange && interactAction.WasPressedThisFrame())
        {
            if (!didDialogueStart) StartDialogue();
            else
            {
                if (dialogueText.text == lineaActualProcesada) NextDialogueLine();
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = lineaActualProcesada;
                }
            }
        }
    }

    private void StartDialogue()
    {
        NPCMapa mapa = GetComponentInParent<NPCMapa>();
        string nombreAVisualizar = "Desconocido";
        bool estaDerrotado = false;

        if (mapa != null)
        {
            if (mapa.idNPC < 1000)
            {
                // CORRECCIÓN AQUÍ:
                if (GameManager.Instancia.jugadorActivo != null && GameManager.Instancia.jugadorActivo.enemigosDerrotados != null)
                {
                    var datos = System.Array.Find(GameManager.Instancia.jugadorActivo.enemigosDerrotados, e => e.id_npc == mapa.idNPC);
                    if (datos != null)
                    {
                        nombreAVisualizar = datos.nombre;
                        estaDerrotado = datos.derrotado;
                    }
                }
            }
            else
            {
                nombreAVisualizar = "Aldeano";
            }
        }

        if (npcNameText != null) npcNameText.text = nombreAVisualizar;
        lineasActivas = estaDerrotado ? lineasDerrotado : lineasNormales;

        if (lineasActivas == null || lineasActivas.Length == 0) return;

        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        if (audioAmbienteDialogo != null && ControladorSonido.Instance != null)
        {
            // Usamos el canal de música porque ya tiene Loop y se puede detener
            ControladorSonido.Instance.EjecutarSonido(audioAmbienteDialogo);
        }


        lineIndex = 0;
        BloquearMovimiento(true);
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < lineasActivas.Length) StartCoroutine(ShowLine());
        else CerrarDialogo();
    }

    private void CerrarDialogo()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        if (dialogueMark != null) dialogueMark.SetActive(true);
        BloquearMovimiento(false);

        if(ControladorSonido.Instance != null) ControladorSonido.Instance.StopSFX();
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        string lineaOriginal = lineasActivas[lineIndex];
        string nombreReal = (GameManager.Instancia.jugadorActivo != null) ? GameManager.Instancia.jugadorActivo.nombre : "Jugador";
        string npcNombre = npcNameText != null ? npcNameText.text : "NPC";

        lineaActualProcesada = lineaOriginal.Replace("{PlayerName}", nombreReal).Replace("{NPCName}", npcNombre);

        foreach (char ch in lineaActualProcesada)
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (dialogueMark != null) dialogueMark.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (dialogueMark != null) dialogueMark.SetActive(true);
        }
    }

    private void BloquearMovimiento(bool bloquear)
    {
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {
            var mov = jugador.GetComponent<Mover4Direcciones>();
            var anim = jugador.GetComponent<Animator>();
            if (mov != null && anim != null)
            {
                mov.rb.linearVelocity = Vector2.zero;
                if (bloquear)
                {
                    anim.SetFloat("velX", 0);
                    anim.SetFloat("velY", 0);
                    anim.SetFloat("velocidad", 0);
                    anim.SetBool("animAct", false);
                }
                mov.enabled = !bloquear;
            }
        }
    }
}