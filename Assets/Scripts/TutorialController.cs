using System.Collections;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject object1;
    public Collider2D playerCollider;
    public Collider2D object2Collider;

    private bool isFadingOut = false;
    private Camera mainCamera;

    private void Start()
    {
        // Hide object1 at start
        object1.SetActive(false);
        // Get the main camera component
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // Set object1's position to the center of the camera view
        Vector3 cameraCenter = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, mainCamera.nearClipPlane));
        object1.transform.position = new Vector2(cameraCenter.x, cameraCenter.y+1);

        // Check if the player collider is currently overlapping with the object2 collider
        if (playerCollider.IsTouching(object2Collider))
        {
            // Show object1 if it is not already visible
            if (!object1.activeSelf)
            {
                object1.SetActive(true);
            }
            // Cancel the fade-out coroutine if it is currently running
            if (isFadingOut)
            {
                StopCoroutine("FadeOut");
                isFadingOut = false;
            }
        }
        else
        {
            // Start the fade-out coroutine if it is not currently running
            if (!isFadingOut && object1.activeSelf)
            {
                StartCoroutine("FadeOut");
                isFadingOut = true;
            }
        }
    }

    private IEnumerator FadeOut()
    {
        // Get the sprite renderer component of object1
        SpriteRenderer spriteRenderer = object1.GetComponent<SpriteRenderer>();

        // Fade out the alpha value of the sprite over 1.5 seconds
        float time = 1.5f;
        while (time > 0)
        {
            Color color = spriteRenderer.color;
            color.a = time / 1.5f;
            spriteRenderer.color = color;
            yield return null;
            time -= Time.deltaTime;
        }

        // Hide object1 after the fade-out is complete
        object1.SetActive(false);
    }
}
