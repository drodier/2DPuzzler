using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CollectiblesController : MonoBehaviour
{
    public Image imageToChange; // the image to change color
    public Color newColor; // the new color to set the image to
    public AudioClip pickupSound; // the sound to play when the item is picked up
    public static int totalBookCount = 0; // the total count of collected items
    public static List<GameObject> collectedItems = new List<GameObject>(); // list of collected items

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Change the color of the image
            //imageToChange.color = newColor;

            // Play the pickup sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Check if the item has already been collected
            if (!collectedItems.Contains(gameObject))
            {
                totalBookCount++; // increase the total item count

                collectedItems.Add(gameObject); // add the collected item to the list
            }

            gameObject.SetActive(false); // disable the collected item object
        }
    }

    public static void ClearCollectedItems()
    {
        collectedItems.Clear(); // clear the collected items list
    }
}