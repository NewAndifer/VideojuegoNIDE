using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class IdentificadorPuerta : MonoBehaviour
{
    [Header("Datos de Base de Datos")]
    public int idPuerta = -1;
    public bool abierta = false;
    public int costo = 500;

    [Header("Visuales de la Valla")]
    [SerializeField] private GameObject puertaBloqueada;
    [SerializeField] private GameObject puertaDesbloqueada;

    [Header("Referencia al Script Dinámico")]
    // Arrastra aquí el objeto hijo que tiene el script 'LetreroDinamico'
    [SerializeField] private LetreroDinamico miLetrero;

    [Header("Input")]
    [SerializeField] private InputAction interactAction;

    private bool isPlayerInRange;

    private void OnEnable() => interactAction.Enable();
    private void OnDisable() => interactAction.Disable();

    void Start()
    {
        DescargarDatosPuerta();
        SetEstadosPuertas();
        // Limpiamos por si acaso quedó algo en el prefab
        if (miLetrero != null) miLetrero.Limpiar();
    }

    void Update()
    {
        if (isPlayerInRange && !abierta && interactAction.WasPressedThisFrame())
        {
            IntentarCompra();
        }
    }

    private void IntentarCompra()
    {
        // SEGURIDAD: Checar si el GameManager existe
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null)
        {
            Debug.LogError("No hay GameManager o Jugador para cobrar!");
            return;
        }

        var jugador = GameManager.Instancia.jugadorActivo;

        if (jugador.monedas >= costo)
        {
            jugador.monedas -= costo;

            // 2. Marcamos como abierta
            abierta = true;

            // 3. Actualizamos la "Base de Datos" (el array del jugador)
            ActualizarPuertaEnData();

            // 4. Cambiamos los GameObjects (apaga madera, prende paso libre)
            SetEstadosPuertas();

            // 5. Limpiamos el letrero para que no flote nada en el aire
            if (miLetrero != null) miLetrero.Limpiar();

            Debug.Log($"Puerta {idPuerta} desbloqueada. Monedas restantes: {jugador.monedas}");
        }
        else
        {
            // FALLO DE MONEDAS
            if (miLetrero != null)
            {
                // Mandamos "R" como tecla y el mensaje exacto que quieres como acción
                // El 'true' al final fuerza la limpieza para que NO se encimen
miLetrero.AgregarOpcion("", "Monedas insuficientes", Color.white, true);

                // Regresa al texto normal después de 2 segundos
                Invoke("RestaurarLetreroNormal", 2f);
            }
        }
    }

    // --- Control de Rango (Llamado por el Sensor del Hijo) ---
    public void SetPlayerInRange(bool value)
    {
        isPlayerInRange = value;

        if (miLetrero == null || abierta) return;

        if (value)
        {
            // Usamos tu función: Presiona R para desbloquear (Cuesta 500)
            miLetrero.AgregarOpcion("R", $"desbloquear (${costo})");
        }
        else
        {
            miLetrero.Limpiar();
        }
    }

    private void RestaurarLetreroNormal()
    {
        if (isPlayerInRange && !abierta && miLetrero != null)
        {
            miLetrero.Limpiar();
            miLetrero.AgregarOpcion("R", $"desbloquear (${costo})");
        }
    }

    // --- Tus métodos de persistencia ---
    public void SetEstadosPuertas()
    {
        puertaDesbloqueada.SetActive(abierta);
        puertaBloqueada.SetActive(!abierta);
    }

    private void DescargarDatosPuerta()
    {
        if (idPuerta < 0 || GameManager.Instancia?.jugadorActivo == null) return;
        foreach (var p in GameManager.Instancia.jugadorActivo.puertasAbiertas)
        {
            if (p.id_puerta == idPuerta) { abierta = p.abierta; break; }
        }
    }

    private void ActualizarPuertaEnData()
    {
        List<Puerta> lista = new List<Puerta>(GameManager.Instancia.jugadorActivo.puertasAbiertas);
        bool encontrada = false;
        foreach (var p in lista) { if (p.id_puerta == idPuerta) { p.abierta = true; encontrada = true; break; } }
        if (!encontrada) lista.Add(new Puerta { id_puerta = idPuerta, abierta = true });
        GameManager.Instancia.jugadorActivo.puertasAbiertas = lista.ToArray();
    }
}