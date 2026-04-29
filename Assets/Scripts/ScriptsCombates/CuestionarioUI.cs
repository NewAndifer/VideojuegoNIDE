using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CuestionarioUI : MonoBehaviour
{
    private Label labelVidas;
    private Label labelAciertos;
    private Label labelPreguntaMatematica;
    private Button[] botonesRespuesta;

    // Elementos de la pantalla final
    private VisualElement panelFinal;
    private Label labelMensajeFinal;
    private Button botonContinuar;

    // Variable para guardar la acción de forma segura en memoria
    private Action accionContinuarPendiente;

    public void Inicializar(VisualElement root)
    {
        labelVidas = root.Q<Label>("NumPregunta");
        labelAciertos = root.Q<Label>("NumProgreso");
        labelPreguntaMatematica = root.Q<Label>("Pregunta");

        var qList = root.Query<Button>(null, "boton").ToList();
        
        botonesRespuesta = qList.ToArray();

        panelFinal = root.Q<VisualElement>("PanelFinal");
        labelMensajeFinal = root.Q<Label>("MensajeFinal");
        botonContinuar = root.Q<Button>("BotonContinuar");

        if (panelFinal != null) panelFinal.style.display = DisplayStyle.None;

        // Asignamos el evento de clic UNA SOLA VEZ aquí
        if (botonContinuar != null)
        {
            botonContinuar.clicked += OnBotonContinuarClick;
        }
    }

    public void ActualizarVidas(int actuales, int maximas)
    {
        if (labelVidas != null) labelVidas.text = $"{actuales}/{maximas}";
    }

    public void ActualizarProgreso(int actuales, int meta)
    {
        if (labelAciertos != null) labelAciertos.text = $"{actuales}/{meta}";
    }

    public void MostrarPregunta(string texto)
    {
        if (labelPreguntaMatematica != null) labelPreguntaMatematica.text = texto;
    }

    public void ConfigurarBoton(int indice, string texto, bool habilitado)
    {
        if (indice < botonesRespuesta.Length)
        {
            botonesRespuesta[indice].text = texto;
            botonesRespuesta[indice].SetEnabled(habilitado);
        }
    }

    public void AplicarColorBoton(int indice, Color color)
    {
        if (indice < botonesRespuesta.Length)
        {
            botonesRespuesta[indice].style.unityBackgroundImageTintColor = new StyleColor(color);
        }
    }

    public void LimpiarColores()
    {
        foreach (var btn in botonesRespuesta)
        {
            btn.style.unityBackgroundImageTintColor = StyleKeyword.Null;
        }
    }

    public void MostrarPantallaFinal(string mensaje, Action accionContinuar)
    {
        if (panelFinal != null && labelMensajeFinal != null)
        {
            labelMensajeFinal.text = mensaje;
            panelFinal.style.display = DisplayStyle.Flex; 
            
            // Guardamos la instrucción de cambiar de escena
            accionContinuarPendiente = accionContinuar;
        }
        else
        {
            accionContinuar(); 
        }
    }

    // Este método es disparado por el botón y ejecuta la acción guardada
    private void OnBotonContinuarClick()
    {
        Debug.Log("Botón Continuar presionado, cambiando de escena...");
        accionContinuarPendiente?.Invoke();
    }
}