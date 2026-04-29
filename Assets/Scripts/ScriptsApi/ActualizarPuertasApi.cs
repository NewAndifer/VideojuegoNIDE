using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ActualizarPuertasApi : MonoBehaviour
{
    [Header("Configuracion de API")]
    [SerializeField] private string baseUrl;
    [SerializeField] private string endpoint;

    public struct DatosJugadorPuertas
    {
        public int idEstudiante;
        public int idPuerta;
    }

    public IEnumerator ActualizarPuertaAPI(int id_Puerta)
    {
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay GameManager o Jugador para la puerta!");
            yield break;
        }

        DatosJugadorPuertas datos = new DatosJugadorPuertas
        {
            idEstudiante = GameManager.Instancia.jugadorActivo.id,
            idPuerta = id_Puerta
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
                Debug.LogError("Error al actualizar puerta: " + request.error);
            }
            else
            {
                Debug.Log("Puerta actualizada correctamente");
            }
        }

    }
}
