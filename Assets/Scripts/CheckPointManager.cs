using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public Sprite activeSprite;
    public Sprite inactiveSprite;
    public bool isActive = false;

    private SpriteRenderer spriteRenderer;
    private GameObject player;

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

            isActive = true;
            spriteRenderer.sprite = activeSprite;
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
