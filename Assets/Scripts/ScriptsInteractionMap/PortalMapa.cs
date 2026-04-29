using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalMapa : MonoBehaviour
{
    [Header("Selección de mapa")]
    [SerializeField] private string operacion;

    private string escena;

    void Start()
    {
        escena = (operacion == "division") ? "MapaDivision" : (operacion == "multiplicacion") ? "MapaMultiplicacion" : (operacion == "resta") ? "MapaResta" : "MapaSuma" ; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(escena);
        }
        
    }


}
