using UnityEngine;
using TMPro;

public class LetreroDinamico : MonoBehaviour
{
    // Definimos los tipos de interacción para automatizar colores o íconos
    public enum TipoInteraccion { NPC, Puerta, Cofre, Sistema }

    [SerializeField] private GameObject botonPrefab;
    [SerializeField] private Transform canvasPadre;
    
    [Header("Colores por Defecto")]
    [SerializeField] private Color colorNormal = Color.black;
    [SerializeField] private Color colorError = Color.white;
    [SerializeField] private Color colorEspecial = new Color(0.1f, 0.5f, 0.1f); // Verde para éxito

    /// <summary>
    /// Versión PRO: Detecta el tipo de objeto para aplicar lógica visual automáticamente.
    /// </summary>
    public void AgregarOpcion(string tecla, string accion, TipoInteraccion tipo)
    {
        Color colorAUsar = colorNormal;

        // Lógica de diseño según el tipo
        switch (tipo)
        {
            case TipoInteraccion.Puerta:
                colorAUsar = Color.black; // O un café oscuro
                break;
            case TipoInteraccion.Sistema:
                colorAUsar = colorError;
                break;
            case TipoInteraccion.Cofre:
                colorAUsar = new Color(0.8f, 0.5f, 0f); // Dorado
                break;
        }

        AgregarOpcion(tecla, accion, colorAUsar);
    }

    /// <summary>
    /// Función base con override de color manual.
    /// </summary>
    public void AgregarOpcion(string tecla, string accion, Color? colorTexto = null, bool limpiarAnteriores = false)
{
    if (botonPrefab == null || canvasPadre == null) return;

    // Si le pedimos que limpie (caso de la puerta), lo hace. 
    // Si no (caso del NPC), los botones se van apilando.
    if (limpiarAnteriores) 
    {
        Limpiar(); 
    }

    GameObject nuevoBoton = Instantiate(botonPrefab, canvasPadre);
    nuevoBoton.transform.localScale = Vector3.one;
    nuevoBoton.transform.localPosition = Vector3.zero;

    TMP_Text texto = nuevoBoton.GetComponentInChildren<TMP_Text>();
    if (texto != null)
    {
        texto.enableVertexGradient = false;
        Color colorFinal = colorTexto ?? colorNormal;
        
        if (string.IsNullOrEmpty(tecla)) 
            texto.text = accion;
        else 
            texto.text = $"Presiona {tecla} para {accion}";
        
        texto.color = colorFinal;
        texto.faceColor = colorFinal;
        texto.SetAllDirty();
    }
}
    // Sobrecarga clásica para compatibilidad
    public void AgregarOpcion(string tecla, string accion) 
        => AgregarOpcion(tecla, accion, null);

    public void Limpiar()
    {
        if (canvasPadre == null) return;
        foreach (Transform hijo in canvasPadre) Destroy(hijo.gameObject);
    }
}