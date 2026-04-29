using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Regresar : MonoBehaviour
{
    private UIDocument menu;
    private Button botonRegresar;

    void OnEnable()
    {
        menu = GetComponent<UIDocument>();
        var root = menu.rootVisualElement;
        botonRegresar = root.Q<Button>("BotonRegresar");
        botonRegresar.clicked += CerrarEscena;
    }

    private void CerrarEscena()
    {
        if (ControladorSonido.Instance != null)
            ControladorSonido.Instance.StopMusica();

        if (GameManager.Instancia != null)
        {
            SceneManager.LoadScene(GameManager.Instancia.ultimaEscenaMapa);
        }
        else
        {
            SceneManager.LoadScene("Crossroad");
        }
    }
    private void OnDisable()
    {
        if (botonRegresar != null)
            botonRegresar.clicked -= CerrarEscena;
    }
}
