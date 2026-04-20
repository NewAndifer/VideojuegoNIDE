using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem; // ¡IMPORTANTE para Unity 6!

public class NPCInteraction : MonoBehaviour
{
    [Header("Referencias")]
    public UIDocument uiDocument;
    
    [Header("Contenido")]
    [TextArea(3, 5)]
    public string[] instructionalCards;
    
    private VisualElement dialogueBox;
    private Label messageLabel;
    private int currentCardIndex = 0;
    private bool isPlayerInRange = false;
    private bool isDialogueActive = false;

    void Start()
    {
        if (uiDocument == null) uiDocument = GetComponentInChildren<UIDocument>();
        
        if (uiDocument != null)
        {
            var root = uiDocument.rootVisualElement;
            dialogueBox = root.Q<VisualElement>("DialogueBox"); // Revisa que en UI Builder se llame exactamente así
            messageLabel = root.Q<Label>("MessageText"); // Revisa que en UI Builder se llame exactamente así

            if (dialogueBox != null) dialogueBox.style.display = DisplayStyle.None;
        }
        else
        {
            Debug.LogError("--- [ERROR] No hay UIDocument en " + name);
        }
    }

    void Update()
    {
        // Detectamos la tecla E usando el Nuevo Sistema de Entrada
        if (isPlayerInRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("--- [INPUT] Se presionó la tecla E");
            HandleInteraction();
        }
    }

    void HandleInteraction()
    {
        if (!isDialogueActive)
        {
            Debug.Log("--- [LOGIC] Iniciando diálogo...");
            isDialogueActive = true;
            currentCardIndex = 0;
            dialogueBox.style.display = DisplayStyle.Flex;
            UpdateUI();
        }
        else
        {
            currentCardIndex++;
            if (currentCardIndex < instructionalCards.Length)
            {
                Debug.Log("--- [LOGIC] Pasando a tarjeta " + currentCardIndex);
                UpdateUI();
            }
            else
            {
                Debug.Log("--- [LOGIC] Fin de la explicación.");
                CloseDialogue();
            }
        }
    }

    void UpdateUI()
    {
        if (messageLabel != null) 
            messageLabel.text = instructionalCards[currentCardIndex];
    }

    void CloseDialogue()
    {
        isDialogueActive = false;
        dialogueBox.style.display = DisplayStyle.None;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("--- [PHYSICS] Jugador en rango.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            CloseDialogue();
            Debug.Log("--- [PHYSICS] Jugador salió de rango.");
        }
    }
}