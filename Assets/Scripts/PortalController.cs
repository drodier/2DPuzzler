using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private bool isReturn = false;
    private bool isPlayerInside = false; // flag to keep track of whether the player is inside the portal
    private bool isLocked = false;

    private Color lockedColor = new Color(.5f, .5f, .5f);
    private Color unlockedColor = new Color(1, 1, 1);

    void FixedUpdate()
    {
        isLocked = !GetComponent<ActivatableContent>().GetStatus();

        GetComponent<SpriteRenderer>().color = isLocked ? lockedColor : unlockedColor;
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
