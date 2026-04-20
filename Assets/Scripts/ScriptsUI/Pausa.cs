using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Pausa : MonoBehaviour
{
    public bool estaPausado = false;
    private VisualElement menuPausa;
    private Button botonPausa;
    private Button botonContinuar;
    private Button botonMenuPrincipal;
    private VisualElement contenedorPrincipal;

    // Cambiamos el nombre para que sea más claro
    private InputAction accionPausa;

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        menuPausa = root.Q<VisualElement>("ContenedorMenuPausa");
        contenedorPrincipal = root.Q<VisualElement>("ContenedorPrincipal");
        botonPausa = root.Q<Button>("BotonPausa");
        botonContinuar = root.Q<Button>("BotonContinuar");  
        botonMenuPrincipal = root.Q<Button>("BotonMenuPrincipal");

        menuPausa.style.visibility = Visibility.Hidden;

        accionPausa = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/escape");
        accionPausa.performed += ctx => AlternarPausa();
        botonPausa.clicked += AlternarPausa;
        botonContinuar.clicked += AlternarPausa;
        botonMenuPrincipal.clicked += cambiarMenuPrincipal;


        accionPausa.Enable();
    }

    void OnDisable()
    {
        if (accionPausa != null)
        {
            accionPausa.Disable();
            accionPausa.performed -= ctx => AlternarPausa();
            botonPausa.clicked -= AlternarPausa;
            botonContinuar.clicked -= AlternarPausa;
            botonMenuPrincipal.clicked -= cambiarMenuPrincipal;
        }
        
    }

    public void AlternarPausa()
    {
        estaPausado = !estaPausado;
        menuPausa.style.visibility = estaPausado ? Visibility.Visible : Visibility.Hidden;

        contenedorPrincipal.style.backgroundColor = estaPausado ? new StyleColor(new Color(0f, 0f, 0f, 0.7f)) : new StyleColor(Color.clear);
        Time.timeScale = estaPausado ? 0f : 1f;
    }

    private void cambiarMenuPrincipal()
    {
        AlternarPausa();
        SceneManager.LoadScene("Menu");
    }
    
}