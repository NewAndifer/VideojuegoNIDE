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
        if(ControladorSonido.Instance != null) ControladorSonido.Instance.StopMusica();
        
        SceneManager.LoadScene("Mainmap_01");
    }
    private void OnDisable()
    {
        botonRegresar.clicked -= CerrarEscena;
    }
}
