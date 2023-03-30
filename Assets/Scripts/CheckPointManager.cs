using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private Transform[] checkpoints;
    private int currentCheckpointIndex;

    void Start()
    {
        // Find all the checkpoint objects in the scene
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpoints = new Transform[checkpointObjects.Length];
        for (int i = 0; i < checkpointObjects.Length; i++)
        {
            checkpoints[i] = checkpointObjects[i].transform;
        }

        // Set the current checkpoint to the first checkpoint in the list
        currentCheckpointIndex = 0;
    }


    public void SetCurrentCheckpoint(Transform checkpoint)
    {
        // Get the index of the checkpoint in the list
        int index = System.Array.IndexOf(checkpoints, checkpoint);

        // If the checkpoint is found in the list, update the current checkpoint index
        if (index >= 0)
        {
            currentCheckpointIndex = index;
        }
    }

    public void RespawnPlayer()
    {
        // Get the current checkpoint transform
        Transform checkpoint = checkpoints[currentCheckpointIndex];

        // Get the player's transform
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // Move the player to the checkpoint position and reset its velocity
        player.transform.position = checkpoint.position;
        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
}
