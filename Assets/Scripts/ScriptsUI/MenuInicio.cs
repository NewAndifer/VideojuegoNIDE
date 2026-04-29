using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuInicio : MonoBehaviour
{
    private UIDocument menu;
    private Button botonIniciar;
    private Button botonSalir;
    private Button botonTutorial;
    private Button botonOpciones;
    private Button botonCreditos;

    private TutorialManager tutorialManager;

    [Header("UI Secundarias")]
    [SerializeField] private Creditos Creditos;
    // --- NUEVO: Referencia directa al UIDocument de Opciones ---
    [SerializeField] private UIDocument documentoOpciones;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;

        botonIniciar = root.Q<Button>("BotonIniciar");
        botonSalir = root.Q<Button>("BotonSalir");
        botonTutorial = root.Q<Button>("BotonTutorial");
        botonCreditos = root.Q<Button>("BotonCreditos");
        botonOpciones = root.Q<Button>("BotonOpciones");

        tutorialManager = FindAnyObjectByType<TutorialManager>();

        if (botonIniciar != null) botonIniciar.clicked += IniciarJuego;
        if (botonSalir != null) botonSalir.clicked += CerrarSesion;
        if (botonTutorial != null) botonTutorial.clicked += AbrirTutorial;
        if (botonCreditos != null) botonCreditos.clicked += () => Creditos.MostrarCreditos();
        if (botonOpciones != null) botonOpciones.clicked += AbrirOpciones;

        // Asegurarnos de que el menú de opciones inicie apagado y no estorbe
        if (documentoOpciones != null)
        {
            documentoOpciones.rootVisualElement.style.display = DisplayStyle.None;
        }
    }

    void Start()
    {
        AbrirTutorial();
    }

    private void AbrirTutorial()
    {
        if (tutorialManager != null)
        {
            tutorialManager.IniciarTutorial();
        }
        else
        {
            Debug.LogWarning("No se encontró el TutorialManager en la escena.");
        }
    }

    private void IniciarJuego()
    {
        SceneManager.LoadScene("Crossroads");
    }

    // --- LO QUE FALTABA ---
    private void AbrirOpciones()
    {
        // Solo prendemos el menú de opciones (como una capa superior)
        if (documentoOpciones != null)
        {
            documentoOpciones.rootVisualElement.style.display = DisplayStyle.Flex;
        }
    }

    

    private void OnDisable()
    {
        if (botonIniciar != null) botonIniciar.clicked -= IniciarJuego;
        if (botonSalir != null) botonSalir.clicked -= CerrarSesion;
        if (botonTutorial != null) botonTutorial.clicked -= AbrirTutorial;

        // CORREGIDO: Tenías cruzados los nombres aquí
        if (botonCreditos != null) botonCreditos.clicked -= () => Creditos.MostrarCreditos();
        if (botonOpciones != null) botonOpciones.clicked -= AbrirOpciones;
    }

    private void CerrarSesion()
    {
        GameManager.Instancia.jugadorActivo = null;
        GameManager.Instancia.idEnemigoActual = -1;
        GameManager.Instancia.operacionActual = "";
        GameManager.Instancia.tipoNPCActual = "";
        GameManager.Instancia.nombreNPCActual = "";
        GameManager.Instancia.posicionRetornoMapa = Vector3.zero;

        SceneManager.LoadScene("LogIn");
    }
}