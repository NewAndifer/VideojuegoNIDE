using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pausa : MonoBehaviour
{
    public bool estaPausado = false;

    private VisualElement contenedorPrincipal;
    private VisualElement menuPausa;
    private VisualElement panelOpciones;

    private Button botonPausa;
    private Button botonContinuar;
    private Button botonOpciones;
    private Button botonVolver;
    private Button botonMenuPrincipal;

    private Slider sliderMusica;
    private Slider sliderSFX;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        contenedorPrincipal = root.Q<VisualElement>("ContenedorPrincipal");
        menuPausa = root.Q<VisualElement>("ContenedorMenuPausa");
        panelOpciones = root.Q<VisualElement>("ContenedorOpciones");

        botonPausa = root.Q<Button>("BotonPausa");
        botonContinuar = root.Q<Button>("BotonContinuar");
        botonOpciones = root.Q<Button>("BotonOpciones");
        botonVolver = root.Q<Button>("BotonVolver");
        botonMenuPrincipal = root.Q<Button>("BotonMenuPrincipal");

        sliderMusica = root.Q<Slider>("SliderMusica");
        sliderSFX = root.Q<Slider>("SliderEfectosSonido"); // Revisa si le pusiste este nombre
        
        // 2. Registramos el evento de cambio de valor
        if (sliderMusica != null)
            sliderMusica.RegisterValueChangedCallback(OnCambioMusica);

        if (sliderSFX != null)
            sliderSFX.RegisterValueChangedCallback(OnCambioSFX);

        ReanudarJuego();

        botonPausa.clicked += PausarJuego;
        botonContinuar.clicked += ReanudarJuego;

        botonOpciones.clicked += AbrirOpciones;
        botonVolver.clicked += CerrarOpciones;
        botonMenuPrincipal.clicked += IrAlMenu;
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (estaPausado)
            {
                ReanudarJuego();
            }
            else
            {
                PausarJuego();
            }
        }
    }


    public void PausarJuego()
    {
        if (estaPausado) return;

        estaPausado = true;
        Time.timeScale = 0f;

        contenedorPrincipal.style.backgroundColor = new StyleColor(new Color(0, 0, 0, 0.7f));
        menuPausa.style.display = DisplayStyle.Flex;
        panelOpciones.style.display = DisplayStyle.None;
        botonPausa.style.display = DisplayStyle.None;
    }

    public void ReanudarJuego()
    {
        if (!estaPausado) return;

        estaPausado = false;
        Time.timeScale = 1f;

        contenedorPrincipal.style.backgroundColor = new StyleColor(Color.clear);
        menuPausa.style.display = DisplayStyle.None;
        panelOpciones.style.display = DisplayStyle.None;
        botonPausa.style.display = DisplayStyle.Flex;
    }


    private void AbrirOpciones()
    {
        menuPausa.style.display = DisplayStyle.None;
        panelOpciones.style.display = DisplayStyle.Flex;
    }

    private void CerrarOpciones()
    {
        panelOpciones.style.display = DisplayStyle.None;
        menuPausa.style.display = DisplayStyle.Flex;
    }

    private void IrAlMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    void OnDisable()
    {
        if (botonPausa != null) botonPausa.clicked -= PausarJuego;
        if (botonContinuar != null) botonContinuar.clicked -= ReanudarJuego;
        if (botonOpciones != null) botonOpciones.clicked -= AbrirOpciones;
        if (botonVolver != null) botonVolver.clicked -= CerrarOpciones;
        if (botonMenuPrincipal != null) botonMenuPrincipal.clicked -= IrAlMenu;
        if (sliderMusica != null)
            sliderMusica.UnregisterValueChangedCallback(OnCambioMusica);

        if (sliderSFX != null)
            sliderSFX.UnregisterValueChangedCallback(OnCambioSFX);
    }

    private void OnCambioMusica(ChangeEvent<float> evt)
    {
        if (ControladorSonido.Instance != null)
        {
            // Corregido: SetVolumenMusica en lugar de AjustarVolumenMusica
            ControladorSonido.Instance.SetVolumenMusica(evt.newValue);
        }
    }

    private void OnCambioSFX(ChangeEvent<float> evt)
    {
        if (ControladorSonido.Instance != null)
        {
            // Corregido: SetVolumenSFX en lugar de AjustarVolumenSFX
            ControladorSonido.Instance.SetVolumenSFX(evt.newValue);
        }
    }
}