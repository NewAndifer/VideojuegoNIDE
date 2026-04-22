using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System;
using Random = UnityEngine.Random;


public class Preguntas : MonoBehaviour
{
    public UIDocument uiDocument;
    private CuestionarioUI uiScript;

    public int dificultad = 1;
    public string operacion = "suma";
    public Button[] replyButtons;
    private int idxPreguntaCorrecta = -1;
    private bool listenersAssigned = false;
    private int preguntasRespondidas = 0;
    public int aciertosAcumulados = 0;
    public int maxAciertos = 10;
    public int vidas = 5;
    public int vidasMaximas = 5;
    public float tiempoEspera = 1f;
    private CuestionarioUI ui;
    private List<Button> registeredButtons = new List<Button>();

    public static event Action OnAcierto;
    public static event Action OnFallo;

    private bool isEsperando = false;


    void OnEnable()
    {
        if (GameManager.Instancia != null && GameManager.Instancia.jugadorActivo != null)
        {
            string diffStr = GameManager.Instancia.jugadorActivo.dificultad;
            dificultad = (diffStr == "Fácil") ? 1 : (diffStr == "Media") ? 2 : 3;
            operacion = GameManager.Instancia.operacionActual;
        }

        uiDocument = GetComponent<UIDocument>();
        uiScript = GetComponent<CuestionarioUI>();
        uiScript.Inicializar(uiDocument.rootVisualElement);

        var root = uiDocument.rootVisualElement;
        var qList = root.Query<Button>().ToList();
        replyButtons = qList != null ? qList.ToArray() : new Button[0];

        ActualizarTodoElHUD();


        if (!listenersAssigned)
        {
            for (int i = 0; i < replyButtons.Length; i++)
            {
                var btn = replyButtons[i];
                if (btn == null) continue;
                btn.userData = i;
                btn.RegisterCallback<UnityEngine.UIElements.ClickEvent>(OnButtonClicked);
                registeredButtons.Add(btn);
            }
            listenersAssigned = true;
        }

        SetupQuestion();
    }

    void ActualizarTodoElHUD()
    {
        uiScript.ActualizarVidas(vidas, vidasMaximas);
        uiScript.ActualizarProgreso(aciertosAcumulados, maxAciertos);
    }

    void OnDisable()
    {
        foreach (var btn in registeredButtons)
        {
            if (btn == null) continue;
            btn.UnregisterCallback<UnityEngine.UIElements.ClickEvent>(OnButtonClicked);
        }
        registeredButtons.Clear();
        listenersAssigned = false;
    }

    void SetupQuestion()
    {
        if (vidas <= 0 || aciertosAcumulados >= maxAciertos)
        {
            CerrarEscena();
            return;
        }

        var pregunta = GeneradorPreguntas.Generar(dificultad, operacion);

        uiScript.MostrarPregunta(pregunta.textoOperacion);
        int resultadoReal = pregunta.resultado;

        idxPreguntaCorrecta = Random.Range(0, replyButtons.Length);
        HashSet<int> respuestasUsadas = new HashSet<int> { resultadoReal };

        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (i == idxPreguntaCorrecta)
            {
                uiScript.ConfigurarBoton(i, resultadoReal.ToString(), true);
            }
            else
            {
                int falsa;
                do
                {
                    falsa = Mathf.Abs(resultadoReal + Random.Range(-5, 6));
                } while (respuestasUsadas.Contains(falsa));

                respuestasUsadas.Add(falsa);
                uiScript.ConfigurarBoton(i, falsa.ToString(), true);
            }
        }
    }

    void OnReplyButtonClicked(int index)
    {

        preguntasRespondidas++;

        if (index == idxPreguntaCorrecta)
        {
            botonCorrecto(index);
        }
        else
        {
            botonIncorrecto(index);
        }


        StartCoroutine(MostrarColoresYEsperar());
    }

    private void OnButtonClicked(UnityEngine.UIElements.ClickEvent evt)
    {
        var btn = evt.currentTarget as Button;
        if (btn == null) return;
        if (btn.userData == null) return;
        int idx = (int)btn.userData;
        preguntasRespondidas++;
        OnReplyButtonClicked(idx);
    }

    public void botonIncorrecto(int index)
    {
        vidas--;
        uiScript.ActualizarVidas(vidas, vidasMaximas); // Actualizar pantalla
        OnFallo?.Invoke();
    }

    public void botonCorrecto(int index)
    {
        aciertosAcumulados++;
        uiScript.ActualizarProgreso(aciertosAcumulados, maxAciertos); // Actualizar pantalla
        OnAcierto?.Invoke();
    }

    private void CerrarEscena()
    {
        if (aciertosAcumulados >= maxAciertos)
        {
            DarRecompensa();
        }

        SceneManager.LoadScene("Mainmap_01");
    }

    private IEnumerator MostrarColoresYEsperar()
    {
        isEsperando = true;
        for (int i = 0; i < replyButtons.Length; i++)
        {
            replyButtons[i].SetEnabled(false);

            // Delegamos el color al script de UI
            Color colorResultado = (i == idxPreguntaCorrecta) ? Color.green : Color.red;
            uiScript.AplicarColorBoton(i, colorResultado);
        }

        // 2. Pausa dramática
        yield return new WaitForSeconds(tiempoEspera);

        // 3. Limpiamos los colores usando el método que creamos en CuestionarioUI
        uiScript.LimpiarColores();

        isEsperando = false;

        // 4. Siguiente pregunta
        SetupQuestion();
    }
    void DarRecompensa()
    {
        int idBuscado = GameManager.Instancia.idEnemigoActual;
        bool encontrado = false;

        foreach (var e in GameManager.Instancia.jugadorActivo.enemigosDerrotados)
        {
            if (e.id == idBuscado)
            {
                if (!e.derrotado)
                {
                    GameManager.Instancia.jugadorActivo.monedas += 300;
                    e.derrotado = true;
                }
                encontrado = true;
                break;
            }
        }

        if (!encontrado)
        {
            GameManager.Instancia.jugadorActivo.monedas += 300;

            Enemigo nuevoEnemigo = new Enemigo { id = idBuscado, derrotado = true };

            var listaTemporal = new List<Enemigo>(GameManager.Instancia.jugadorActivo.enemigosDerrotados);
            listaTemporal.Add(nuevoEnemigo);
            GameManager.Instancia.jugadorActivo.enemigosDerrotados = listaTemporal.ToArray();

            Debug.Log($"Nuevo NPC {idBuscado} derrotado. +300 monedas.");
        }
    }
}
