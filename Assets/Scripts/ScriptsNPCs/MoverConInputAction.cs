using UnityEngine;
using UnityEngine.InputSystem; 

public class Mover4Direcciones : MonoBehaviour
{
    [SerializeField] 
    private InputAction accionMover; 

    [SerializeField] 
    private float velocidad = 5f; 
    
    void Start()
    {
        accionMover.Enable();
    }

    void Update()
    {
        Vector2 direccion = accionMover.ReadValue<Vector2>();
        transform.position = CalcularNuevaPosicion(transform.position, direccion, velocidad, Time.deltaTime);
    }

    public Vector2 CalcularNuevaPosicion(Vector2 posicionActual, Vector2 direccionInput, float vel, float deltaTiempo)
    {
        return posicionActual + (direccionInput * vel * deltaTiempo);
    }
}