using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f; // Player move speed
    [SerializeField] private float runSpeed = 15f; // Player run speed
    [SerializeField] private float jumpForce = 10f; // Player jump force
    [SerializeField] private float variableJumpHeightMultiplier = 0.5f; // Jump height multiplier when holding jump button longer
    [SerializeField] private float wallJumpForce = 8f; // Force applied to player when wall jumping
    [SerializeField] private float wallSlideSpeed = 3f; // Speed at which player slides down walls
    [SerializeField] private float wallStickTime = 0.25f; // Time player sticks to wall after leaving input direction
    [SerializeField] private int wallJumpDirection = 1; // Direction player jumps when wall jumping

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects
    [SerializeField] private Transform groundCheck; // Transform object for checking if player is on ground
    [SerializeField] private float groundCheckRadius = 0.2f; // Radius of ground check circle

    [Header("Wall Settings")]
    [SerializeField] private LayerMask wallLayer; // Layer mask for wall objects
    [SerializeField] private Transform wallCheck; // Transform object for checking if player is touching wall
    [SerializeField] private float wallCheckDistance = 0.5f; // Distance of wall check raycast

    [Header("Animation Settings")]
    [SerializeField] private Animator anim; // Player animator component
    [SerializeField] private string animSpeedParamName = "Speed"; // Name of animator parameter for player speed
    [SerializeField] private string animIsGroundedParamName = "IsGrounded"; // Name of animator parameter for player grounded status
    [SerializeField] private string animIsRunningParamName = "IsRunning"; // Name of animator parameter for player running status
    [SerializeField] private string animIsWallSlidingParamName = "IsWallSliding"; // Name of animator parameter for player wall sliding status

    private Rigidbody2D rb; // Player rigidbody component
    private SpriteRenderer spriteRenderer; // Player sprite renderer component
    private float horizontalInput; // Input value for player horizontal movement
    private bool isGrounded; // Flag indicating if player is on ground
    private bool isRunning; // Flag indicating if player is running
    private bool isJumping; // Flag indicating if player is jumping
    private bool isWallSliding; // Flag indicating if player is wall sliding
    private bool isTouchingWall; // Flag indicating if player is touching wall
    private int wallDirection; // Direction of wall player is touching
    private float timeToWallUnstick; // Time until player can unstick from wall

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // Check if player is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Check if player is touching wall
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, wallLayer) || Physics2D.Raycast(wallCheck.position, -
