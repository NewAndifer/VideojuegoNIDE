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
    public Label labelPregunta;
    public Label labelProgreso;
    public int dificultad;


    private Label opLabel;
    private List<int> aleatorio = new List<int>();
    public string operacion;
    public Button[] replyButtons;
    private int idxPreguntaCorrecta = -1;
    private bool listenersAssigned = false;
    private int preguntasRespondidas = 0;
    public int aciertosAcumulados = 0;
    public int maxAciertos = 10;
    public int vidas = 5;
    public int vidasMaximas = 5;
    private List<Button> registeredButtons = new List<Button>();

    public static event Action OnAcierto;
    public static event Action OnFallo;

    private bool isEsperando = false;


    void OnEnable()
    {

        uiDocument = GetComponent<UIDocument>();


        var root = uiDocument.rootVisualElement;

        labelPregunta = root.Q<Label>("NumPregunta");
        labelPregunta.text = $"{vidas}/{vidasMaximas}";

        labelProgreso = root.Q<Label>("NumProgreso");
        labelProgreso.text = $"{aciertosAcumulados}/{maxAciertos}";

        var qList = root.Query<Button>().ToList();
        replyButtons = qList != null ? qList.ToArray() : new Button[0];
        opLabel = root.Q<Label>("Pregunta");

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
        if (vidas <= 0)
        {
            foreach (var b in replyButtons)
            {
                if (b == null) continue;
                b.SetEnabled(false);
            }
            CerrarEscena();
            Debug.Log("Cuestionario finalizado.");
            return;
        }

        if (aciertosAcumulados >= maxAciertos)
        {
            foreach (var b in replyButtons)
            {
                if (b == null) continue;
                b.SetEnabled(false);
            }
            CerrarEscena();
            Debug.Log("Cuestionario finalizado.");
            return;
        }

        aleatorio = numerosAleatorios(dificultad, operacion);

        string opNorm = (operacion ?? "").Trim().ToLower();
        if (opNorm == "resta" || opNorm == "division")
        {
            aleatorio.Sort();
            aleatorio.Reverse();
        }

        opLabel.text = stringOperation(operacion, aleatorio);

        idxPreguntaCorrecta = Random.Range(0, replyButtons.Length);
        Debug.Log($"Índice pregunta correcta: {idxPreguntaCorrecta}");

        var respuestasUsadas = new HashSet<int>(replyButtons.Length + 1);
        int resultadoReal = resultOperation(operacion, aleatorio);
        respuestasUsadas.Add(resultadoReal);

        for (int i = 0; i < replyButtons.Length; i++)
        {
            var btn = replyButtons[i];
            if (btn == null) continue;

            if (i == idxPreguntaCorrecta)
            {
                btn.text = resultadoReal.ToString();
                btn.SetEnabled(true);
            }
            else
            {
                int respuestaFalsa = resultadoReal;
                int intentos = 0;
                do
                {
                    respuestaFalsa = resultadoReal + Random.Range(-5, 6);
                    respuestaFalsa = Mathf.Abs(respuestaFalsa);
                    intentos++;
                }
                while (respuestasUsadas.Contains(respuestaFalsa) && intentos < 50);

                respuestasUsadas.Add(respuestaFalsa);
                btn.text = respuestaFalsa.ToString();
                btn.SetEnabled(true);
            }
        }
    }

    void OnReplyButtonClicked(int index)
    {
        preguntasRespondidas++;
        if (labelPregunta != null)
            labelPregunta.text = $"{vidas}/{vidasMaximas}";


        for (int i = 0; i < replyButtons.Length; i++)
        {
            if (index == idxPreguntaCorrecta)
            {

            }

        }


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

    List<int> numerosAleatorios(int dificultad, string op)
    {
        List<int> listaNumeros = new List<int>();
        int numPorGenerar = 2;
        int rango = 10;

        switch (op)
        {
            case "suma":
                switch (dificultad)
                {
                    case 1:
                        rango = 10;
                        break;

                    case 2:
                        rango = 100;
                        break;

                    case 3:
                        rango = 1000;
                        break;
                }
                break;


            case "resta":
                switch (dificultad)
                {
                    case 1:
                        rango = 10;
                        break;

                    case 2:
                        rango = 100;
                        break;

                    case 3:
                        rango = 1000;
                        break;
                }
                break;


            case "multiplicacion":
                switch (dificultad)
                {
                    case 1:
                        rango = 10;
                        break;

                    case 2:
                        rango = 50;
                        break;

                    case 3:
                        rango = 100;
                        break;
                }
                break;

            case "division":
                switch (dificultad)
                {
                    case 1:
                        rango = 10;
                        break;

                    case 2:
                        rango = 20;
                        break;

                    case 3:
                        rango = 50;
                        break;
                }
                break;

            default:
                Debug.LogWarning($"Dificultad desconocida: {dificultad}, usando rango por defecto {rango}.");
                break;
        }

        for (int i = 0; i < numPorGenerar; i++)
        {
            listaNumeros.Add(Random.Range(1, rango + 1));
        }

        if (op == "division")
        {
            listaNumeros.Sort();
            listaNumeros.Reverse();
            int dividendo = 1;
            for (int i = 0; i < listaNumeros.Count; i++)
            {
                dividendo *= listaNumeros[i];
            }

            listaNumeros[0] = dividendo;

        }

        return listaNumeros;
    }

    int resultOperation(string op, List<int> numAleatorios)
    {

        if (op == null) op = "";
        op = op.Trim().ToLower();

        int resultado = 0;
        switch (op)
        {
            case "suma":
                resultado = 0;
                for (int i = 0; i < numAleatorios.Count; i++)
                {
                    resultado += numAleatorios[i];
                }
                break;

            case "resta":
                numAleatorios.Sort();
                numAleatorios.Reverse();
                resultado = numAleatorios[0];
                for (int i = 1; i < numAleatorios.Count; i++)
                {
                    resultado -= numAleatorios[i];
                }

                break;

            case "multiplicacion":
                resultado = 1;
                for (int i = 0; i < numAleatorios.Count; i++)
                {
                    resultado *= numAleatorios[i];
                }
                break;

            case "division":
                resultado = numAleatorios[0];
                for (int i = 1; i < numAleatorios.Count; i++)
                {
                    resultado /= numAleatorios[i];
                }
                break;



            default:
                Debug.LogWarning($"Operación desconocida: {op}");
                break;
        }

        return resultado;
    }

    string stringOperation(string op, List<int> numAleatorios)
    {

        if (op == null) op = "";
        op = op.Trim().ToLower();

        char charOperator = '-';
        var sb = new StringBuilder();

        switch (op)
        {
            case "suma":
                charOperator = '+';
                break;

            case "resta":
                charOperator = '-';
                break;

            case "multiplicacion":
                charOperator = 'x';
                break;

            default:
                charOperator = '/';
                break;
        }

        for (int i = 0; i < numAleatorios.Count - 1; i++)
        {
            sb.Append(numAleatorios[i].ToString());
            sb.Append(charOperator);
        }

        if (numAleatorios.Count > 0)
            sb.Append(numAleatorios[numAleatorios.Count - 1].ToString());

        return sb.ToString();
    }

    void botonIncorrecto(int index)
    {
        vidas--;
        if (labelPregunta != null)
            labelPregunta.text = $"{vidas}/{vidasMaximas}";

        // ¡AVISO!: "Alguien falló"
        OnFallo?.Invoke();
    }

    void botonCorrecto(int index)
    {
        aciertosAcumulados++;
        if (labelProgreso != null)
            labelProgreso.text = $"{aciertosAcumulados}/{maxAciertos}";

        // ¡AVISO!: "Alguien acertó, hagan lo que tengan que hacer"
        OnAcierto?.Invoke();
    }

    private void CerrarEscena()
    {
        SceneManager.LoadScene("Mainmap_01");
    }

    private IEnumerator MostrarColoresYEsperar()
    {
        isEsperando = true; // Bloqueamos los clics

        // Coloreamos los botones y los deshabilitamos temporalmente
        for (int i = 0; i < replyButtons.Length; i++)
        {
            var btn = replyButtons[i];
            if (btn == null) continue;

            btn.SetEnabled(false); // Deshabilitar mientras esperamos

            if (i == idxPreguntaCorrecta)
            {
                btn.style.unityBackgroundImageTintColor = new StyleColor(Color.green);
            }
            else
            {
                btn.style.unityBackgroundImageTintColor = new StyleColor(Color.red);
            }
        }


        yield return new WaitForSeconds(1f);

        for (int i = 0; i < replyButtons.Length; i++)
        {
            var btn = replyButtons[i];
            if (btn == null) continue;

            btn.style.unityBackgroundImageTintColor = StyleKeyword.Null;
        }

        isEsperando = false;
        SetupQuestion();
    }
}
