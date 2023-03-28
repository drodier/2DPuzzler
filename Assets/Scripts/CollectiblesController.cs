using UnityEngine;
using UnityEngine.UI;

public class CollectiblesController : MonoBehaviour
{
    public Image imagePrefab; // the image prefab to instantiate
    public Transform canvasTransform; // the canvas transform to add the image to
    public Vector2 offset; // the offset from the top left corner of the canvas

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // instantiate the image at the top left corner of the canvas with the specified offset
            Image image = Instantiate(imagePrefab, Vector3.zero, Quaternion.identity);
            image.transform.SetParent(canvasTransform, false);
            image.rectTransform.anchoredPosition = offset;

            Destroy(gameObject); // destroy the collectible object
        }
    }
}
