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
        }
    }

    public void ResetCheckpoint()
    {
        isActive = false;
        spriteRenderer.sprite = inactiveSprite;
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
