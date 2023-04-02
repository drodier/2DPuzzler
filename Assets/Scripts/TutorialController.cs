using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject object1;
    public Collider2D playerCollider;
    public Collider2D object2Collider;

    private void Start()
    {
        // Hide object1 at start
        object1.SetActive(false);
    }

    private void Update()
    {
        // Check if the player collider is currently overlapping with the object2 collider
        if (playerCollider.IsTouching(object2Collider))
        {
            // Show object1 if it is not already visible
            if (!object1.activeSelf)
            {
                object1.SetActive(true);
            }
        }
        else
        {
            // Hide object1 if it is currently visible
            if (object1.activeSelf)
            {
                object1.SetActive(false);
            }
        }
    }
}
