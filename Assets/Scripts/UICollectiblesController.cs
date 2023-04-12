using UnityEngine;
using UnityEngine.UI;

public class UICollectiblesController : MonoBehaviour
{
    public int imageNumber; // the number of the image controlled by this script
    public Color collectedColor; // the color to set the image to when collected
    public Color uncollectedColor; // the color to set the image to when not collected

    private Image image;

    void Start()
    {
        // Get the Image component attached to the same GameObject
        image = GetComponent<Image>();

        // Set the initial color of the image based on the totalBookCount
        if (CollectiblesController.totalBookCount >= imageNumber)
        {
            image.color = collectedColor;
        }
        else
        {
            image.color = uncollectedColor;
        }
    }

    void Update()
    {
        // Update the color of the image based on the totalBookCount
        if (CollectiblesController.totalBookCount >= imageNumber)
        {
            image.color = collectedColor;
        }
        else
        {
            image.color = uncollectedColor;
        }
    }
}
