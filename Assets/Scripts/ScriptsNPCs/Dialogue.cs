using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] TMP_Text dialogueText;

    [Header("Input")]
    [SerializeField] private InputAction interactAction;


    private bool isPlayerInRange;
    private bool didDialogueStart;
    private int lineIndex = 0;
    private float typingTime = 0.05f;

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    void Update()
    {
        if (isPlayerInRange && interactAction.WasPressedThisFrame())
        {
            if (!didDialogueStart)
            {
                StartDialogue();
            }
            else
            {
                if (dialogueText.text == dialogueLines[lineIndex])
                {
                    NextDialogueLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = dialogueLines[lineIndex];
                }
            }
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        dialogueMark.SetActive(false);
        lineIndex = 0;

        BloquearMovimiento(true);
        StartCoroutine(ShowLine());
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            CerrarDialogo();
        }

    }

    private void CerrarDialogo()
    {
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        dialogueMark.SetActive(true);
        BloquearMovimiento(false);
    }

    private IEnumerator ShowLine()
    {
        dialogueText.text = string.Empty;
        foreach (char ch in dialogueLines[lineIndex])
        {
            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            Debug.Log("Se puede iniciar un dialogo");

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            isPlayerInRange = false;
            dialogueMark.SetActive(false);
            Debug.Log("No se puede iniciar un dialogo");
        }
    }

    private void BloquearMovimiento(bool bloquear)
    {
        GameObject jugador = GameObject.FindWithTag("Player");
        if (jugador != null)
        {

            var mov = jugador.GetComponent<Mover4Direcciones>();
            if (mov != null) mov.enabled = !bloquear;
        }
    }


}
