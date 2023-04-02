using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Character move speed
    [SerializeField] private float jumpForce = 10f; // Character jump force
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private float wallJumpForce = 10f; // Character wall jump force
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects
    [SerializeField] private Transform groundCheck; // Transform object for checking if character is on ground
    [SerializeField] private LayerMask wallLayer; // Layer mask for wall objects

    private Rigidbody2D rb; // Character rigidbody component
    private Animator anim; // Character animator component
    private bool isGrounded; // Flag indicating if character is on ground
    private bool isSprinting = false;
    private bool isMoving = false;
    private bool isFalling = false;
    private bool isJumping = false;
    private int currentRoom = 0;
    private GrabableObject heldItem = null;
    private AudioSource audioSource;
    public AudioClip moveSound;
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip pickUpSound;
    public AudioClip dropSound;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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

            if (!isFalling)
            {
                audioSource.PlayOneShot(landSound);
            }
        }

        if(heldItem != null)
        {
            if(transform.localScale.x == 1)
            {
                heldItem.transform.position = transform.position + new Vector3(.3f, 0, 0);
                //heldItem.transform.localScale = new Vector3(0.1590151f, heldItem.transform.localScale.y, heldItem.transform.localScale.z);
                heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                heldItem.transform.position = transform.position + new Vector3(-.3f, 0, 0);
                //heldItem.transform.localScale = new Vector3(-0.1590151f, heldItem.transform.localScale.y, heldItem.transform.localScale.z);
                heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }

        }

            // Check if character is touching a wall and apply downwards force
            bool isTouchingWall = GetComponent<Collider2D>().IsTouchingLayers(wallLayer);
            if (isTouchingWall && !isGrounded && !isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, -moveSpeed/2);
            }
    }

    // Function to be called by Animation Event to play sound
    public void PlayMoveSound()
    {
        audioSource.clip = moveSound;
        audioSource.Play();
    }

    private void Update()
    {
        // Check if character is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        bool isTouchingWall = Physics2D.Raycast(transform.position, transform.right, 0.5f, wallLayer) || Physics2D.Raycast(transform.position, -transform.right, 0.5f, wallLayer);
        isSprinting = isGrounded ? Input.GetKey(KeyCode.LeftShift) : false;

        // Character jump input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isJumping = true; // Set jumping flag to true
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                // Play jump sound
                audioSource.PlayOneShot(jumpSound);
            }
            else if (isTouchingWall)
            {
                isJumping = true; // Set jumping flag to true
                float wallJumpDirection = transform.localScale.x > 0 ? -1 : 1; // determine which direction to jump off the wall
                rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpForce);

                audioSource.PlayOneShot(jumpSound);
            }
        }
            // Reset jumping flag when character lands on ground
            if (isGrounded)
            {
                isJumping = false;
            }

        if(Input.GetKeyUp(KeyCode.F) && IsHolding())
            DropItem();

        // Update animator parameters
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void ChangeCurrentRoom(int newRoom)
    {
        currentRoom = newRoom;
    }

        public void PickUpItem(GrabableObject item)
        {
            if(!IsHolding())
            {
                heldItem = item;
                heldItem.StartLockout();

                audioSource.PlayOneShot(pickUpSound);
            }
        }

        public GrabableObject DropItem()
        {
            GrabableObject dropped = null;

            if(!heldItem.IsLockedOut())
            {
                heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                dropped = heldItem;
                heldItem = null;

                audioSource.PlayOneShot(dropSound);
            }

            return dropped;
        }

        public bool IsHolding()
        {
            return heldItem != null;
        }
}