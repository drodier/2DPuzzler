using UnityEngine;
using UnityEngine.UI;

public class CollectiblesController : MonoBehaviour
{
    public Image imagePrefab; // the image prefab to instantiate
    public Transform canvasTransform; // the canvas transform to add the image to
    public Vector2 offset = new Vector2(0, 0); // the offset from the top left corner of the canvas
    public AudioClip pickupSound; // the sound to play when the item is picked up

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Image image = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity);
            image.transform.SetParent(canvasTransform, false);
            image.rectTransform.anchorMin = new Vector2(0, 1); // set the anchor point to the top left corner
            image.rectTransform.anchorMax = new Vector2(0, 1); // set the anchor point to the top left corner
            image.rectTransform.pivot = new Vector2(0, 1); // set the pivot point to the top left corner
            image.rectTransform.anchoredPosition = offset; // set the offset from the top left corner of the canvas

            // Play the pickup sound
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject); // destroy the collectible object
        }
    }
}
