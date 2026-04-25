// using UnityEngine;
// using UnityEngine.InputSystem; 

// public class Mover4Direcciones : MonoBehaviour
// {
//     [SerializeField] 
//     private InputAction accionMover; 

//     [SerializeField] 
//     private float velocidad = 5f; 
    
//     void Start()
//     {
//         accionMover.Enable();
//     }

//     void Update()
//     {
//         Vector2 direccion = accionMover.ReadValue<Vector2>();
//         transform.position = CalcularNuevaPosicion(transform.position, direccion, velocidad, Time.deltaTime);
//     }

//     public Vector2 CalcularNuevaPosicion(Vector2 posicionActual, Vector2 direccionInput, float vel, float deltaTiempo)
//     {
//         return posicionActual + (direccionInput * vel * deltaTiempo);
//     }
// }

using UnityEngine;
using UnityEngine.InputSystem;

public class Mover4Direcciones : MonoBehaviour
{
    public InputAction accionMover; 
    public float velocidadX = 5f;
    public float velocidadY = 5f;

    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        accionMover.Enable();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 movimiento = accionMover.ReadValue<Vector2>();

        rb.linearVelocityX = movimiento.x * velocidadX; 
        rb.linearVelocityY = movimiento.y * velocidadY; 
        ActualizarAnimaciones(movimiento);
    }

    private void ActualizarAnimaciones(Vector2 movimiento)
    {
        float velocidadActual = rb.linearVelocity.magnitude;
        animator.SetFloat("velX", movimiento.x); 
        animator.SetFloat("velY", movimiento.y); 
        animator.SetFloat("velocidad", movimiento.magnitude);

    }
}