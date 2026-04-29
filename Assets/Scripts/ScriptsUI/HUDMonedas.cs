using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class HUDMonedas : MonoBehaviour
{
    [Header("Configuración de UI")]
    public UIDocument uiDocument;
    public string nombreIconoMoneda = "IconoMoneda";
    public string nombreTextoMonedas = "NumMonedas";
    public string nombreBotonMapa = "BotonMapa";

    [Header("Animación del Sprite")]
    public Sprite[] framesAnimacion;
    public float cuadrosPorSegundo = 12f;

    private Image iconoUI;
    private Label textoUI;
    private int frameActual = 0;
    private float temporizador = 0f;
    private Button botonMapa;

    void OnEnable()
    {
        if (uiDocument == null) uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        iconoUI = root.Q<Image>(nombreIconoMoneda);
        textoUI = root.Q<Label>(nombreTextoMonedas);
        botonMapa = root.Q<Button>(nombreBotonMapa);

        string escenaActual = SceneManager.GetActiveScene().name;

        if (escenaActual == "Crossroads")
        {
            botonMapa.style.display = DisplayStyle.None;
        }
        else
        {
            botonMapa.style.display = DisplayStyle.Flex;
            botonMapa.clicked += TransportarMapa;
        }

        SincronizarConGameManager();
    }

    void OnDisable()
    {
        if (botonMapa != null)
        {
            botonMapa.clicked -= TransportarMapa;
        }
    }

    void Update()
    {
        AnimarMoneda();

        if (GameManager.Instancia != null && GameManager.Instancia.jugadorActivo != null)
        {
            ActualizarTextoMonedas(GameManager.Instancia.jugadorActivo.monedas);
        }
    }

    private void SincronizarConGameManager()
    {
        if (GameManager.Instancia != null && GameManager.Instancia.jugadorActivo != null)
        {
            int monedasReales = GameManager.Instancia.jugadorActivo.monedas;
            ActualizarTextoMonedas(monedasReales);
        }
    }

    private void AnimarMoneda()
    {
        if (framesAnimacion == null || framesAnimacion.Length == 0 || iconoUI == null) return;

        temporizador += Time.deltaTime;
        float tiempoPorFrame = 1f / cuadrosPorSegundo;

        if (temporizador >= tiempoPorFrame)
        {
            temporizador -= tiempoPorFrame;
            frameActual = (frameActual + 1) % framesAnimacion.Length;

            iconoUI.sprite = framesAnimacion[frameActual];
        }
    }

    public void ActualizarTextoMonedas(int cantidadActual)
    {
        if (textoUI != null)
        {
            textoUI.text = cantidadActual.ToString();
        }
    }

    public void TransportarMapa()
    {
        SceneManager.LoadScene("Crossroads");
    }
}