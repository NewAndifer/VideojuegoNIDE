using UnityEngine;
using UnityEngine.UIElements;

public class HUDMonedas : MonoBehaviour
{
    [Header("Configuración de UI")]
    public UIDocument uiDocument;
    public string nombreIconoMoneda = "IconoMoneda";
    public string nombreTextoMonedas = "NumMonedas";

    [Header("Animación del Sprite")]
    public Sprite[] framesAnimacion;
    public float cuadrosPorSegundo = 12f;

    private Image iconoUI;
    private Label textoUI;
    private int frameActual = 0;
    private float temporizador = 0f;

    void OnEnable()
    {
        if (uiDocument == null) uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;

        iconoUI = root.Q<Image>(nombreIconoMoneda);
        textoUI = root.Q<Label>(nombreTextoMonedas);

        SincronizarConGameManager();
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
}