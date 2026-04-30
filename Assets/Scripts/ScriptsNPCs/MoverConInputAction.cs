
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