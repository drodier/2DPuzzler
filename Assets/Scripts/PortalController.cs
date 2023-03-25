using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private KeyCode portalKey = KeyCode.Space; // The key that the player needs to press to use the portal

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(portalKey))
        {
            Vector3 newPosition = other.transform.position + new Vector3(1000f, 0f, 0f);
            other.transform.position = newPosition;
        }
    }
}
