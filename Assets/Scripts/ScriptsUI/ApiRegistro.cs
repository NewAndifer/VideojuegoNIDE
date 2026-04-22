using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Networking; // Indispensable para la conexión

public class APIIRegistro : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button btnEnviarDatos;
    private Label Estatus;
    private TextField tfID;
    private TextField tfContrasena;

    [Header("Configuración de API")]
    public string url;

    [System.Serializable]
    public struct Credenciales
    {
        public int id;
        public string password;
    }

    void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        btnEnviarDatos = root.Q<Button>("EnviarDatos");
        tfID = root.Q<TextField>("TfID");
        tfContrasena = root.Q<TextField>("TfContrasena");
        Estatus = root.Q<Label>("Estatus");

        btnEnviarDatos.clicked += OnLoginClicked;
    }

    void OnDisable()
    {
        if (btnEnviarDatos != null)
            btnEnviarDatos.clicked -= OnLoginClicked;
    }

    private void OnLoginClicked()
    {
        string id = tfID.value;
        string contrasena = tfContrasena.value;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(contrasena))
        {
            Estatus.text = "Completa todos los campos";
            return;
        }

        btnEnviarDatos.SetEnabled(false);
        Estatus.text = "Validando datos...";

        StartCoroutine(Autenticacion(id, contrasena));
    }

    IEnumerator Autenticacion(string idString, string contrasena)
    {
        if (!int.TryParse(idString, out int idConvertido))
        {
            Estatus.text = "ID debe ser numérico";
            btnEnviarDatos.SetEnabled(true);
            yield break;
        }

        Credenciales p = new Credenciales { id = idConvertido, password = contrasena };
        string datosJson = JsonUtility.ToJson(p);
        Debug.Log("Enviando JSON: " + datosJson);

        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(datosJson);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonRespuesta = request.downloadHandler.text;

                DatosJugador datos = JsonUtility.FromJson<DatosJugador>(jsonRespuesta);

                GameManager.Instancia.jugadorActivo = datos;

                Estatus.text = "¡Sesión iniciada!";
                print(jsonRespuesta);
                SceneManager.LoadScene("Menu");
            }
            else
            {
                Estatus.text = "Error: Usuario o contraseña incorrectos";
                btnEnviarDatos.SetEnabled(true);
            }
        }
    }

    IEnumerator CambiarEscena()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Menu");
    }
}