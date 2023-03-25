using UnityEngine;

public class PortalController : MonoBehaviour
{
    [SerializeField] private bool isReturn = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 positionOffset = new Vector3(isReturn ? -30f : 30f, 0f, 0f);
            other.transform.position += positionOffset;
            Camera.main.transform.position += positionOffset;
            GameObject.Find("Player").GetComponent<CharacterController>().ChangeCurrentRoom(isReturn ? 0 : 1);
        }
    }
}
