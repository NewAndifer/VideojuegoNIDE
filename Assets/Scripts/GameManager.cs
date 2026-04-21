using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instancia;

    public static GameManager Instancia
    {
        get
        {
            // Si alguien pide la instancia y no existe, la creamos
            if (_instancia == null)
            {
                CrearInstancia();
            }
            return _instancia;
        }
    }

    public DatosJugador jugadorActivo;
    public int idEnemigoActual;
    public string operacionActual;

    // Este atributo hace que se ejecute ANTES de que cargue cualquier escena
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CrearInstancia()
    {
        if (_instancia == null)
        {
            // Buscamos si ya existe uno en la escena (por si lo pusiste manual)
            _instancia = FindFirstObjectByType<GameManager>();

            if (_instancia == null)
            {
                // Si realmente no existe, creamos el GameObject
                GameObject go = new GameObject("GameManager_Global");
                _instancia = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
                Debug.Log("<color=green>GameManager Global Creado Automáticamente</color>");
            }
        }
    }

    private void Awake()
    {
        // Seguridad adicional para el Singleton
        if (_instancia == null)
        {
            _instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instancia != this)
        {
            Destroy(gameObject);
        }
    }
}