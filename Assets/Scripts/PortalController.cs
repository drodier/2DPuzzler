using UnityEngine;

public class PortalController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip portalSound;
    [SerializeField] private bool isReturn = false;
    private bool isPlayerInside = false; // flag to keep track of whether the player is inside the portal
    private bool isLocked = false;

    private Animator animator; // reference to the portal's Animator component

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        isLocked = !GetComponent<ActivatableContent>().GetStatus();

        // No longer changing the color of the portal
        // GetComponent<SpriteRenderer>().color = isLocked ? lockedColor : unlockedColor;
        
        // Instead, trigger the animation based on the lock status
        animator.SetBool("IsLocked", isLocked);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInside && !isLocked)
        {
            isPlayerInside = true; // set flag to true
            Vector3 positionOffset = new Vector3(isReturn ? -28f : 28f, 0f, 0f);
            other.transform.position += positionOffset;
            Camera.main.transform.position += positionOffset;
            GameObject.Find("Player").GetComponent<CharacterController>().ChangeCurrentRoom(isReturn ? 0 : 1);

            audioSource.PlayOneShot(portalSound);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && isPlayerInside)
        {
            isPlayerInside = false; // set flag to false
        }
    }
}
