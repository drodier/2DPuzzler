using UnityEngine;

public class DeathBlock : MonoBehaviour
{
    public AudioClip deathSound;
    public GameObject player;
    private CheckPointManager checkpointManager;
    private AudioSource audioSource;

    void Start()
    {
        // Get the CheckpointManager component from the scene
        checkpointManager = FindObjectOfType<CheckPointManager>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not found on DeathBlock object!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player has collided with the DeathBlock
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
            }
        }
    }
}

