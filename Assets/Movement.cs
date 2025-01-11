using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;         // Viteza de mișcare
    public float jumpForce = 10f;       // Forța de săritură
    public LayerMask groundLayer;       // Layer-ul pentru teren (pentru verificarea dacă e pe sol)
    public Transform groundCheck;       // Transform pentru verificarea contactului cu solul
    public float groundCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Verifică input-ul pentru mișcare
        moveInput = Input.GetAxis("Horizontal");

        // Setează viteza în Rigidbody
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // Actualizează animațiile în funcție de viteza player-ului
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Verifică dacă player-ul este pe sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        // Săritura
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Flip al sprite-ului când se mișcă în stânga
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(4, 4, 3);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(-4, 4, 3);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Vizualizare pentru Ground Check
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
