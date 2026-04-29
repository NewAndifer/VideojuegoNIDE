using UnityEngine;
using UnityEngine.UIElements;

public class Creditos : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocumentCreditos;
    private VisualElement panelOverlay;
    private Button botonCerrar;

    void OnEnable() {
        var root = uiDocumentCreditos.rootVisualElement;
        panelOverlay = root.Q<VisualElement>("credits-overlay");
        botonCerrar = root.Q<Button>("button-close-credits");
        if (botonCerrar != null) botonCerrar.clicked += CerrarCreditos;
        ActualizarEstado(false);
    }

    public void MostrarCreditos() => ActualizarEstado(true);
    public void CerrarCreditos() => ActualizarEstado(false);
    private void ActualizarEstado(bool m) => panelOverlay.style.display = m ? DisplayStyle.Flex : DisplayStyle.None;
}