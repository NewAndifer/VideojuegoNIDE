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

        // Forzamos un inicio limpio
        ReanudarJuego();

        // 1. Asignaciones ABSOLUTAS (Cada botón tiene una sola misión)
        botonPausa.clicked += PausarJuego;
        botonContinuar.clicked += ReanudarJuego;
        
        botonOpciones.clicked += AbrirOpciones;
        botonVolver.clicked += CerrarOpciones;
        botonMenuPrincipal.clicked += IrAlMenu;
    }

    void Update()
    {
        // El Escape actúa como un interruptor inteligente
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

    // --- ESTADOS SEPARADOS ---

    public void PausarJuego()
    {
        // Si ya está pausado, ignoramos para no repetir código
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
    }
}