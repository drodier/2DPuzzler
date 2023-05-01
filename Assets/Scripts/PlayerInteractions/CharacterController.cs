using System.Collections;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // Character move speed
    [SerializeField] private float jumpForce = 10f; // Character jump force
    [SerializeField] private float sprintSpeed = 2f;
    [SerializeField] private float wallJumpForce = 10f; // Character wall jump force
    [SerializeField] private float wallSlideForce = 5f;
    [SerializeField] private LayerMask groundLayer; // Layer mask for ground objects
    [SerializeField] private Transform groundCheck; // Transform object for checking if character is on ground
    [SerializeField] private LayerMask wallLayer; // Layer mask for wall objects

    [SerializeField] private ScaleHandController hoveredHand;
    private Rigidbody2D rb; // Character rigidbody component
    private Animator anim; // Character animator component
    private bool isGrounded; // Flag indicating if character is on ground
    private bool isSprinting = false;
    private bool isMoving = false;
    private bool isFalling = false;
    private bool isJumping = false;
    private int currentRoom = 0;
    public int collectedCount = 0;
    private bool isGrabKeyHeld;
    private float grabHoldTime;
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

    private float GetJumpForce()
    {
        float jumpForce = this.jumpForce;
        if(IsHolding())
        {
            // Reduce jump force based on weight of held item
            jumpForce *= heldItem.GetWeightMultiplier();
        }
        return jumpForce;
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
                heldItem.transform.position = transform.position + new Vector3(.2f + heldItem.GetComponent<SpriteRenderer>().bounds.size.x/2, 0, 0);
                //heldItem.transform.localScale = new Vector3(0.1590151f, heldItem.transform.localScale.y, heldItem.transform.localScale.z);
                heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            else
            {
                heldItem.transform.position = transform.position + new Vector3(-.2f -  heldItem.GetComponent<SpriteRenderer>().bounds.size.x/2, 0, 0);
                //heldItem.transform.localScale = new Vector3(-0.1590151f, heldItem.transform.localScale.y, heldItem.transform.localScale.z);
                heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }

        }

            // Check if character is touching a wall and apply downwards force
            bool isTouchingWall = GetComponent<Collider2D>().IsTouchingLayers(wallLayer) && !isGrounded;
            if (isTouchingWall && !isGrounded && !isJumping)
            {
                    rb.velocity = new Vector2(rb.velocity.x, -wallSlideForce);
            }

    }

    // Function to be called by Animation Event to play sound
    public void PlayMoveSound()
    {
        audioSource.clip = moveSound;
        audioSource.Play();
    }

    private IEnumerator JumpDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isJumping = false;
    }

    private void Update()
    {
        // Check if character is on ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        bool isTouchingWall = Physics2D.Raycast(transform.position, transform.right, 0.5f, wallLayer) || Physics2D.Raycast(transform.position, -transform.right, 0.5f, wallLayer);
        isSprinting = isGrounded ? Input.GetButton("Sprint") : false;

        // Character jump input
        if(Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true; // Set jumping flag to true
                rb.velocity = new Vector2(rb.velocity.x, GetJumpForce()); // Use GetJumpForce() method to calculate jump force

                // Play jump sound
                audioSource.PlayOneShot(jumpSound);
            }
            else if (isTouchingWall)
            {
                isJumping = true; // Set jumping flag to true
                float wallJumpDirection = transform.localScale.x > 0 ? -1 : 1; // determine which direction to jump off the wall
                rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, GetJumpForce()); // Use GetJumpForce() method to calculate jump force
                audioSource.PlayOneShot(jumpSound);
                StartCoroutine(JumpDelay(0.1f));
            }
        }
            // Reset jumping flag when character lands on ground
            if (isGrounded)
            {
                isJumping = false;
            }

            if(IsHolding())
            {
                if(Input.GetButtonUp("Rotate"))
                    heldItem.RotateObject();

                if(Input.GetButtonUp("Interact") && hoveredHand == null)
                {
                    DropItem();
                }
                else if(Input.GetButtonUp("Interact") && hoveredHand != null)
                {
                    hoveredHand.PlaceObject(DropItem());
                }
                else if(Input.GetButtonUp("Throw"))
                {
                    heldItem.ThrowItem();
                }
            }
            else if(hoveredHand != null)
            {
                if(Input.GetButtonUp("Interact") && !IsHolding() && hoveredHand.GetObjectWeight() != 0)
                    heldItem = hoveredHand.DropObject();
            }

        // Update animator parameters
        anim.SetBool("IsMoving", isMoving);
        anim.SetBool("IsGrounded", isGrounded);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "ScaleHand")
            hoveredHand = other.GetComponent<ScaleHandController>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "ScaleHand")
            hoveredHand = null;
    }

    public void ChangeCurrentRoom(int newRoom)
    {
        currentRoom = newRoom;
    }

    private float grabStartTime;
    public void PickUpItem(GrabableObject item)
    {
        if(!IsHolding())
        {
            heldItem = item;
            heldItem.StartLockout();

            grabStartTime = Time.time; // Store the time when the grab key is pressed down

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

    public void PlayDropSound()
    {
        audioSource.PlayOneShot(dropSound);
    }
}