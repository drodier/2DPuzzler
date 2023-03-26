using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Character move speed
    [SerializeField] private float jumpForce = 10f; // Character jump force
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects
    [SerializeField] private Transform groundCheck; // Transform object for checking if character is on ground

    private Rigidbody2D rb; // Character rigidbody component
    private Animator anim; // Character animator component
    private bool isGrounded; // Flag indicating if character is on ground
    private bool isSprinting = false;
    private bool isMoving = false;
    private bool isFalling = false;
    private int currentRoom = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * moveSpeed * (isSprinting ? sprintSpeed : 1), rb.velocity.y);

        // Flip character sprite if moving in opposite direction
        if (horizontalInput > 0)
            transform.localScale = new Vector2(1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector2(-1, 1);

        if(transform.position.y <= -10)
            transform.position = new Vector3(currentRoom == 0 ? -10 : 20, -3.5f, 0);

        // Check if character sprite is moving horizontally
        isMoving = Mathf.Abs(rb.velocity.x) > 0;

        bool falling = (rb.velocity.y < 0);
        if (falling != isFalling)
        {

            isFalling = falling;
            anim.SetBool("IsFalling", isFalling);
            }

    }

    private void Update()
    {
        // Check if character is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        isSprinting = isGrounded ? Input.GetKey(KeyCode.LeftShift) : false;

        // Character jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Update animator parameters
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void ChangeCurrentRoom(int newRoom)
    {
        currentRoom = newRoom;
    }
}