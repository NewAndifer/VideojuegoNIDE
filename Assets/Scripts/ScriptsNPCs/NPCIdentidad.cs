using UnityEngine;

public class NPCIdentidad : MonoBehaviour
{
    [Header("Configuración de Base de Datos")]
    public int idDeBaseDeDatos; 
    public string nombreEnemigo;
    public string tipo;

    [Header("Estado Actual")]
    public bool yaDerrotado;

    void Start()
    {
        ConsultarEstado();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
        
    }

    public void ConsultarEstado()
    {
        if (GameManager.Instancia == null || GameManager.Instancia.jugadorActivo == null) return;

        foreach (var e in GameManager.Instancia.jugadorActivo.enemigosDerrotados)
        {
            if (e.id_npc == idDeBaseDeDatos)
            {
                yaDerrotado = e.derrotado;
                break;
            }
        }

        if (yaDerrotado)
        {
            Debug.Log(nombreEnemigo + " ya fue derrotado anteriormente.");
        }
    }
}