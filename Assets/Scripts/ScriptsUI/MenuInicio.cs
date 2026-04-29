using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuInicio : MonoBehaviour
{
    private UIDocument menu;
    private Button botonIniciar;
    private Button botonSalir;
    private Button botonTutorial; // Referencia al nuevo botón

    private TutorialManager tutorialManager; // Referencia al script del tutorial

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        
        botonIniciar = root.Q<Button>("BotonIniciar");
        botonSalir = root.Q<Button>("BotonSalir");
        botonTutorial = root.Q<Button>("BotonTutorial"); // Buscamos el botón en el XML

        // Buscamos el TutorialManager en la escena
        tutorialManager = FindAnyObjectByType<TutorialManager>();

        if (botonIniciar != null) botonIniciar.clicked += IniciarJuego;
        if (botonSalir != null) botonSalir.clicked += CerrarSesion;
        if (botonTutorial != null) botonTutorial.clicked += AbrirTutorial;
    }

    void Start()
    {
        // Esto hará que el tutorial se abra automáticamente al cargar la escena
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

    private void OnDisable()
    {
        if (botonIniciar != null) botonIniciar.clicked -= IniciarJuego;
        if (botonSalir != null) botonSalir.clicked -= CerrarSesion;
        if (botonTutorial != null) botonTutorial.clicked -= AbrirTutorial;
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