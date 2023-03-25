using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Player move speed
    [SerializeField] private float jumpForce = 10f; // Player jump force
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects
    [SerializeField] private Transform groundCheck; // Transform object for checking if player is on ground

    private Rigidbody2D rb; // Player rigidbody component
    private Animator anim; // Player animator component
    private bool isGrounded; // Flag indicating if player is on ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip player sprite if moving in opposite direction
        if (horizontalInput > 0)
            transform.localScale = new Vector2(1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector2(-1, 1);
    }

    private void Update()
    {
        // Check if player is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        // Player jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Update animator parameters
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        anim.SetBool("IsGrounded", isGrounded);
    }
}