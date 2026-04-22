using UnityEngine;
using UnityEngine.UIElements;

public class CuestionarioUI : MonoBehaviour
{
    private Label labelVidas;
    private Label labelAciertos;
    private Label labelPreguntaMatematica;
    private Button[] botonesRespuesta;

    public void Inicializar(VisualElement root)
    {
        labelVidas = root.Q<Label>("NumPregunta");
        labelAciertos = root.Q<Label>("NumProgreso");
        labelPreguntaMatematica = root.Q<Label>("Pregunta");

        var qList = root.Query<Button>().ToList();
        botonesRespuesta = qList.ToArray();
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
}