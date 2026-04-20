using UnityEngine;
using UnityEngine.UIElements;

public class HUDMonedas : MonoBehaviour
{
    [Header("Configuración de UI")]
    public UIDocument uiDocument;
    public string nombreIconoMoneda = "IconoMoneda"; // El nombre de la Image en UI Builder
    public string nombreTextoMonedas = "TextoMonedas"; // El nombre del Label en UI Builder

    [Header("Animación del Sprite")]
    public Sprite[] framesAnimacion;
    public float cuadrosPorSegundo = 12f;

    // Referencias internas
    private Image iconoUI;
    private Label textoUI;
    private int frameActual = 0;
    private float temporizador = 0f;

    void OnEnable()
    {
        if (uiDocument == null) uiDocument = GetComponent<UIDocument>();
        var root = uiDocument.rootVisualElement;
        
        // Obtenemos los elementos de la interfaz
        iconoUI = root.Q<Image>(nombreIconoMoneda);
        textoUI = root.Q<Label>(nombreTextoMonedas);

        // Inicializamos el texto en 0
        ActualizarTextoMonedas(0);
    }

    void Update()
    {
        AnimarMoneda();
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

    // Llama a esta función desde el script de tu jugador cuando recoja una moneda
    public void ActualizarTextoMonedas(int cantidadActual)
    {
        if (textoUI != null)
        {
            textoUI.text = cantidadActual.ToString(); // Puedes poner algo como: = "x " + cantidadActual;
        }
    }
}