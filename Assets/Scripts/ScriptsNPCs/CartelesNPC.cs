using UnityEngine;

public class CartelesNPC : MonoBehaviour
{
    [SerializeField] private LetreroDinamico miManager;
    [SerializeField] private bool mostrarDerecha = true;
    [SerializeField] private bool bandido = false;

    void Awake() 
{
    // Si la casilla está vacía (como pasa en los prefabs)
    if (miManager == null) 
    {
        // Busca en la escena actual cualquier objeto que tenga el script LetreroDinamico
        miManager = GameObject.FindFirstObjectByType<LetreroDinamico>();
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            miManager.Limpiar(); // Limpia botones viejos

            miManager.AgregarOpcion("E", "hablar");

            if (bandido)
            {
                miManager.AgregarOpcion("R", "combatir");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Verificamos que sea el jugador el que se está yendo
        if (other.CompareTag("Player"))
        {
            // Llamamos a la función Limpiar para que borre los botones
            // y el letrero de madera desaparezca de la cabeza del NPC
            if (miManager != null)
            {
                miManager.Limpiar();
            }
        }
    }





}