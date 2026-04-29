using UnityEngine;
using UnityEngine.UIElements;

public class Opciones : MonoBehaviour
{
    private VisualElement contenedorPrincipal;
    private Slider sliderMusica;
    private Slider sliderSFX;
    private Button botonVolver;

    [Header("Conexión con el Menú Principal")]
    [Tooltip("Arrastra aquí el UIDocument de tu Menú Principal para poder volver a encenderlo")]
    public UIDocument menuPrincipalDocument;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        // Referencias usando los nombres que acabamos de agregar al UXML
        contenedorPrincipal = root.Q<VisualElement>("ContenedorPrincipal");
        sliderSFX = root.Q<Slider>("SliderEfectosSonido");
        sliderMusica = root.Q<Slider>("SliderMusica");
        botonVolver = root.Q<Button>("BotonVolver");

        // 1. Sincronizar los valores guardados desde el Singleton
        if (ControladorSonido.Instance != null)
        {
            if (sliderMusica != null) sliderMusica.value = ControladorSonido.Instance.ultimoVolumenMusica;
            if (sliderSFX != null) sliderSFX.value = ControladorSonido.Instance.ultimoVolumenSFX;
        }

        // 2. Suscribir los eventos de los sliders y el botón
        if (sliderMusica != null) sliderMusica.RegisterValueChangedCallback(OnCambioMusica);
        if (sliderSFX != null) sliderSFX.RegisterValueChangedCallback(OnCambioSFX);
        if (botonVolver != null) botonVolver.clicked += CerrarOpciones;
    }

    void OnDisable()
    {
        // Limpieza de memoria
        if (sliderMusica != null) sliderMusica.UnregisterValueChangedCallback(OnCambioMusica);
        if (sliderSFX != null) sliderSFX.UnregisterValueChangedCallback(OnCambioSFX);
        if (botonVolver != null) botonVolver.clicked -= CerrarOpciones;
    }

    // --- MÉTODOS DE SONIDO ---
    private void OnCambioMusica(ChangeEvent<float> evt)
    {
        if (ControladorSonido.Instance != null)
        {
            ControladorSonido.Instance.SetVolumenMusica(evt.newValue);
        }
    }

    private void OnCambioSFX(ChangeEvent<float> evt)
    {
        if (ControladorSonido.Instance != null)
        {
            ControladorSonido.Instance.SetVolumenSFX(evt.newValue);
        }
    }

    // --- NAVEGACIÓN ---
    private void CerrarOpciones()
    {
        // Apagamos exactamente la misma "raíz" que el MenuInicio prende
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.style.display = DisplayStyle.None;
    }
}