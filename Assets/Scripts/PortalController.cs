using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private bool isReturn = false;
    private bool isPlayerInside = false; // flag to keep track of whether the player is inside the portal

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isPlayerInside)
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
