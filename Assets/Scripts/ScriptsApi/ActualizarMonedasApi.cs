using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ActualizarMonedasApi : MonoBehaviour
{
    [Header("Configuración de API")]
    [SerializeField] private string baseUrl;
    [SerializeField] private string endpoint;

    public struct DatosJugadorMonedas
    {
        public int idEstudiante;
        public int nuevoNumeroMonedas;
    }

    public IEnumerator EnviarMonedasAPI()
    {
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay GameManager o Jugador para cobrar!");
            yield break;
        }

        DatosJugadorMonedas datos = new DatosJugadorMonedas
        {
            idEstudiante = GameManager.Instancia.jugadorActivo.id,
            nuevoNumeroMonedas = GameManager.Instancia.jugadorActivo.monedas
        };

        string urlCompleta = baseUrl + endpoint;
        string json = JsonUtility.ToJson(datos);

        using (UnityWebRequest request = new UnityWebRequest(urlCompleta, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error al enviar monedas: " + request.error);
            }
            else
            {
                Debug.Log("Monedas enviadas correctamente.");
            }
        }

    }
}
