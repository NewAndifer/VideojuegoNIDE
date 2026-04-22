using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instancia;

    public static GameManager Instancia
    {
        get
        {
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

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CrearInstancia()
    {
        if (_instancia == null)
        {
            _instancia = FindFirstObjectByType<GameManager>();

            if (_instancia == null)
            {                GameObject go = new GameObject("GameManager_Global");
                _instancia = go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
                Debug.Log("<color=green>GameManager Global Creado Automáticamente</color>");
            }
        }
    }

    private void Awake()
    {
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