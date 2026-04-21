using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class APIIRegistro : MonoBehaviour
{
    private UIDocument uiDocument;
    private Button btnEnviarDatos;
    private Label Estatus;
    private TextField tfID;
    private TextField tfContrasena;
    private string url;

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
        Credenciales p = new Credenciales();

        if (int.TryParse(idString, out int idConvertido))
        {
            p.id = idConvertido;
        }
        else
        {
            Debug.LogError("El ID ingresado no es un número válido.");
            Estatus.text = "ID debe ser numérico";
            btnEnviarDatos.SetEnabled(true);
            yield break;
        }

        p.password = contrasena;

        string datosJson = JsonUtility.ToJson(p);
        Debug.Log("JSON: " + datosJson);
        yield return null;
    }



}