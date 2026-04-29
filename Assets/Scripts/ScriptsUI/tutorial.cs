using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TutorialManager : MonoBehaviour
{
    [System.Serializable]
    public class TutorialStep
    {
        public Sprite imagen;
        [TextArea(3, 10)]
        public string texto;
    }

    [Header("UI Document")]
    [SerializeField] private UIDocument uiDocumentTutorial;

    [Header("Contenido del Tutorial")]
    [SerializeField] private List<TutorialStep> instrucciones;

    private VisualElement root;
    private VisualElement panelOverlay;
    private Image imagenTutorial;
    private Label textoTutorial;
    private Button botonAnterior;
    private Button botonSiguiente;

    private int indiceActual = 0;

    private void OnEnable()
    {
        // 1. Verificación de seguridad
        if (uiDocumentTutorial == null)
        {
            Debug.LogError("Error: Falta asignar el 'UIDocument Tutorial' en el Inspector del TutorialManager.");
            return;
        }

        root = uiDocumentTutorial.rootVisualElement;

        if (root == null)
        {
            Debug.LogError("Error: El rootVisualElement es nulo. Asegúrate de que el UIDocument tiene asignado el 'Tutorial.uxml' en su Source Asset.");
            return;
        }

        // 2. Buscar referencias
        panelOverlay = root.Q<VisualElement>("tutorial-overlay");
        imagenTutorial = root.Q<Image>("tutorial-image");
        textoTutorial = root.Q<Label>("tutorial-text");
        botonAnterior = root.Q<Button>("button-back");
        botonSiguiente = root.Q<Button>("button-next");

        // 3. Verificar si se encontraron los elementos en el XML
        if (botonAnterior == null || botonSiguiente == null || textoTutorial == null)
        {
            Debug.LogError("Error: No se encontraron algunos elementos de UI. Revisa que los 'name' en Tutorial.uxml coincidan exactamente con: 'button-back', 'button-next', 'tutorial-text'.");
            return;
        }

        // 4. Suscribirse a eventos
        botonAnterior.clicked += MostrarPasoAnterior;
        botonSiguiente.clicked += MostrarPasoSiguiente;

        // Ocultar al inicio
        ActualizarEstadoPanel(false);
    }

    private void OnDisable()
    {
        if (botonAnterior != null) botonAnterior.clicked -= MostrarPasoAnterior;
        if (botonSiguiente != null) botonSiguiente.clicked -= MostrarPasoSiguiente;
    }

    public void IniciarTutorial()
    {
        if (instrucciones == null || instrucciones.Count == 0)
        {
            Debug.LogWarning("Advertencia: No hay instrucciones (imágenes/textos) configuradas en el Inspector del TutorialManager.");
            return;
        }

        indiceActual = 0;
        ActualizarContenido();
        ActualizarEstadoPanel(true);
    }

    public void CerrarTutorial()
    {
        ActualizarEstadoPanel(false);
    }

    private void ActualizarEstadoPanel(bool mostrar)
    {
        if (panelOverlay != null)
        {
            panelOverlay.style.display = mostrar ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }

    private void MostrarPasoAnterior()
    {
        if (indiceActual > 0)
        {
            indiceActual--;
            ActualizarContenido();
        }
    }

    private void MostrarPasoSiguiente()
    {
        if (indiceActual < instrucciones.Count - 1)
        {
            indiceActual++;
            ActualizarContenido();
        }
        else
        {
            CerrarTutorial();
        }
    }

    private void ActualizarContenido()
    {
        // Verificación de seguridad extra
        if (instrucciones == null || instrucciones.Count <= indiceActual || textoTutorial == null) return;

        TutorialStep pasoActual = instrucciones[indiceActual];

        if (pasoActual.imagen != null)
        {
            // Asignamos el Sprite directamente al componente Image
            imagenTutorial.sprite = pasoActual.imagen; 
            
            imagenTutorial.style.display = DisplayStyle.Flex;
        }
        else
        {
            imagenTutorial.style.display = DisplayStyle.None;
        }

        textoTutorial.text = pasoActual.texto;
        botonAnterior.SetEnabled(indiceActual > 0);

        if (indiceActual == instrucciones.Count - 1)
        {
            botonSiguiente.text = "Cerrar";
        }
        else
        {
            botonSiguiente.text = "Siguiente";
        }
    }
}