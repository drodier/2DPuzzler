using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float maxCameraSpeed = 3;
    [SerializeField] private float camSpeed = 3;

    private Vector2 desiredPosition;

    void Start()
    {
        // Find the player object dynamically
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Check if the player object exists
        if (player != null)
        {
            desiredPosition = player.transform.position + new Vector3(player.transform.localScale.x, .5f, 0);
        }
    }

    void FixedUpdate()
    {
        // Check if the player object exists
        if (player != null)
        {
            Vector2 direction = Vector2.ClampMagnitude(desiredPosition - new Vector2(transform.position.x, transform.position.y), maxCameraSpeed) * camSpeed * Time.deltaTime;
            transform.position += new Vector3(direction.x, direction.y, 0);
        }
    }
}
