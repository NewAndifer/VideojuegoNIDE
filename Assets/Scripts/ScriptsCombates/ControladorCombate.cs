using UnityEngine;

public class ControladorCombate : MonoBehaviour
{
    [Header("Ajustes de Personajes")]
    public Transform spawnIzquierdo; // Arrastra aquí el objeto vacío del Bandido
    public Transform spawnDerecho;

    [Header("Ajustes de Proyectil")]
    public GameObject prefabBurbuja;
    public float velocidadBurbuja = 10f;
    [Header("Configuracion sfx")]
    [SerializeField] private AudioClip disparo;

    void OnEnable()
    {
        Preguntas.OnAcierto += AtaqueJugador;
        Preguntas.OnFallo += AtaqueEnemigo;
    }

    void OnDisable()
    {
        Preguntas.OnAcierto -= AtaqueJugador;
        Preguntas.OnFallo -= AtaqueEnemigo;
    }

    void AtaqueJugador()
    {
        Disparar(spawnDerecho.position, spawnIzquierdo.position);
    }

    void AtaqueEnemigo()
    {
        Disparar(spawnIzquierdo.position, spawnDerecho.position);
    }

    void Disparar(Vector3 origen, Vector3 destino)
    {
        GameObject burbuja = Instantiate(prefabBurbuja, origen, Quaternion.identity);
        Vector2 direccion = (destino - origen).normalized;

        Rigidbody2D rb = burbuja.GetComponent<Rigidbody2D>();
        ControladorSonido.Instance.EjecutarSonido(disparo);
        if (rb != null) rb.linearVelocity = direccion * velocidadBurbuja;
        Destroy(burbuja, 1.5f);
    }
}