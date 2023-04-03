using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public bool isActive = false;
    public AudioClip checkpointSound;

    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private AudioSource audioSource;
    private int collectedCount = 0;
    private int prevCollectedCount = 0;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = inactiveSprite;

        // Find the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found in scene!");
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
    }

    public void CollectCollectible()
    {
        collectedCount++;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Deactivate all other checkpoints in the scene
            CheckPointManager[] checkpoints = FindObjectsOfType<CheckPointManager>();
            foreach (CheckPointManager checkpoint in checkpoints)
            {
                if (checkpoint != this)
                {
                    checkpoint.ResetCheckpoint();
                }
            }

            if (!isActive) // Check if the checkpoint is not already active
            {
                isActive = true;
                spriteRenderer.sprite = activeSprite;

                // Play the checkpoint sound effect
                if (audioSource != null && checkpointSound != null)
                {
                    audioSource.PlayOneShot(checkpointSound);
                }
            }

            prevCollectedCount = collectedCount; // Store the current collected count
        }
    }

    public void ResetCheckpoint()
    {
        isActive = false;
        spriteRenderer.sprite = inactiveSprite;
        collectedCount = 0;
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            // Find the active checkpoint and respawn the player at its position
            CheckPointManager[] checkpoints = FindObjectsOfType<CheckPointManager>();
            foreach (CheckPointManager checkpoint in checkpoints)
            {
                if (checkpoint.isActive)
                {
                    player.transform.position = checkpoint.transform.position;

                    if (prevCollectedCount > checkpoint.collectedCount)
                    {
                        player.GetComponent<CharacterController>().collectedCount = checkpoint.collectedCount;
                    }
                    else
                    {
                        player.GetComponent<CharacterController>().collectedCount = prevCollectedCount;
                    }

                    break;
                }
            }
        }
        else
        {
            Debug.LogError("Player object not found in scene!");
        }
    }
}
