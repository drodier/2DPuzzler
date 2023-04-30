using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    public AudioClip deathSound;
    public GameObject player;
    private CheckPointManager checkpointManager;
    private AudioSource audioSource;

    void Start()
    {
        // Get the CheckPointManager component from the scene
        checkpointManager = FindObjectOfType<CheckPointManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Play the death sound effect
            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }

            // Respawn the player to the last checkpoint
            if (checkpointManager != null)
            {
                checkpointManager.RespawnPlayer();

                // Reactivate all collected items
                foreach (GameObject item in CollectiblesController.collectedItems)
                {
                    item.SetActive(true);
                }

                CollectiblesController.collectedItems.Clear(); // clear the collected items list
            }
        }
    }
}
