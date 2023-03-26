using UnityEngine;

public class CollectiblesController : MonoBehaviour
{
    public int scoreValue = 1; // the amount of points the collectible is worth

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // add score and destroy the collectible object
            //ScoreController.instance.AddScore(scoreValue);
            Destroy(gameObject);

        }
    }
}
