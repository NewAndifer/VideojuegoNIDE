using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuInicio : MonoBehaviour
{
    private UIDocument menu;
    private Button botonIniciar;
    //private Button botonSalir;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonIniciar = root.Q<Button>("BotonIniciar");
        //botonSalir = root.Q<Button>("BotonSalir");

        botonIniciar.clicked +=  IniciarJuego;
    }

    private void IniciarJuego()
    {
        print("Hola");
        SceneManager.LoadScene("Mainmap_01");
    }

    private void OnDisable()
    {
        botonIniciar.clicked -= IniciarJuego;
    }
}
