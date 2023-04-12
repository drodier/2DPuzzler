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
    public static int savedBookCount = 0; // the total count of collected items at last checkpoint
    public static List<GameObject> savedItems = new List<GameObject>(); // list of saved items at last checkpoint

    private void Start()
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

    private void OnTriggerEnter2D(Collider2D other)
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

                // Store the current value of totalBookCount and collectedItems
                savedBookCount = CollectiblesController.totalBookCount;

                // Create a new list for the saved items
                savedItems = new List<GameObject>();

                // Add the collected items that were picked up after the last checkpoint to the saved items list
                foreach (GameObject item in CollectiblesController.collectedItems)
                {
                    if (item.activeSelf)
                    {
                        savedItems.Add(item);
                    }
                }

                // Destroy all the collected items that were picked up before the checkpoint
                foreach (GameObject item in savedItems)
                {
                    Destroy(item);
                }

                // Clear the collected items list
                CollectiblesController.collectedItems.Clear();

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
                    CollectiblesController.totalBookCount = savedBookCount; // reset totalBookCount to savedBookCount
                    break;
                }
            }

            // Respawn the collected items that were picked up after the last checkpoint
            foreach (GameObject item in savedItems)
            {
                item.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Player object not found in scene!");
        }
    }
}