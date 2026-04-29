using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuInicio : MonoBehaviour
{
    private UIDocument menu;
    private Button botonIniciar;
    private Button botonSalir;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonIniciar = root.Q<Button>("BotonIniciar");
        botonSalir = root.Q<Button>("BotonSalir");

        botonIniciar.clicked +=  IniciarJuego;
        botonSalir.clicked += CerrarSesion;
    }

    private void IniciarJuego()
    {
        SceneManager.LoadScene("Crossroads");
    }

    private void OnDisable()
    {
        botonIniciar.clicked -= IniciarJuego;
        botonSalir.clicked -= CerrarSesion;
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
