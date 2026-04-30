using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class InicioJefe : MonoBehaviour
{
    [Header("Interfaz de Usuario")]
    public UIDocument uiDocument;

    [Header("Scripts del Jefe a Controlar")]
    public EnemyFollow scriptPerseguir;
    public EnemyOrientation scriptOrientacion;
    // Si tienes un script que dispara las burbujas, ponlo aquí también:
    public MonoBehaviour scriptDisparo;

    private Label textoMensaje;

    void Start()
    {
        // 1. "Amárramos" al jefe apagando sus scripts
        if (scriptPerseguir != null) scriptPerseguir.enabled = false;
        if (scriptOrientacion != null) scriptOrientacion.enabled = false;
        if (scriptDisparo != null) scriptDisparo.enabled = false;

        // 2. Enlazamos la UI
        var root = uiDocument.rootVisualElement;
        textoMensaje = root.Q<Label>("TextoMensaje");

        // 3. Iniciamos la secuencia de tiempo
        StartCoroutine(SecuenciaDeInicio());
    }

    private IEnumerator SecuenciaDeInicio()
    {
        // Mostramos las instrucciones iniciales
        textoMensaje.text = "Esquiva las\n burbujas durante\n 1 minuto...";
        yield return new WaitForSeconds(3f); // Esperamos 3 segundos

        // Comienza la cuenta regresiva
        textoMensaje.text = "3";
        yield return new WaitForSeconds(1f);

        textoMensaje.text = "2";
        yield return new WaitForSeconds(1f);

        textoMensaje.text = "1";
        yield return new WaitForSeconds(1f);

        textoMensaje.text = "¡SOBREVIVE!";
        yield return new WaitForSeconds(1f);

        // Ocultamos la interfaz completamente
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;

        // ¡Desatamos al jefe! (Prendemos los scripts)
        if (scriptPerseguir != null) scriptPerseguir.enabled = true;
        if (scriptOrientacion != null) scriptOrientacion.enabled = true;
        if (scriptDisparo != null) scriptDisparo.enabled = true;

        if (BossUIManager.Instance != null)
        {
            BossUIManager.Instance.StartTimer();
        }
    }
}