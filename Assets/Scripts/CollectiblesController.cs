using UnityEngine;
using UnityEngine.UI;

public class CollectiblesController : MonoBehaviour
{
    public Image imageToChange; // the image to change color
    public Color newColor; // the new color to set the image to
    public AudioClip pickupSound; // the sound to play when the item is picked up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointManager checkPointManager = FindObjectOfType<CheckPointManager>();
            if (checkPointManager != null) 
            {
                checkPointManager.CollectCollectible();
            }

            // Change the color of the image
            imageToChange.color = newColor;

            // Play the pickup sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject); // destroy the collectible object
        }
    }
}
