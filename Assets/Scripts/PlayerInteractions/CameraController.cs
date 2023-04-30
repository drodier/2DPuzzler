using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float maxCameraSpeed = 3;
    [SerializeField] private float camSpeed = 3;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;


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
            // Set the camera's desired position above the player's head
            desiredPosition = player.transform.position + new Vector3(player.transform.localScale.x, .5f, 0);

            // Clamp the camera to not exit room borders
            desiredPosition = new Vector2(Mathf.Clamp(desiredPosition.x, minX, maxX), Mathf.Clamp(desiredPosition.y, minY, maxY));
        }
    }

    void FixedUpdate()
    {
        // Check if the player object exists
        if (player != null)
        {
            // Set a direction vector with a clamped magnitude at maxCameraSpeed
            Vector2 direction = Vector2.ClampMagnitude(desiredPosition - new Vector2(transform.position.x, transform.position.y), maxCameraSpeed) * camSpeed * Time.deltaTime;
            // Move camera towards desiredPosition
            transform.position += new Vector3(direction.x, direction.y, 0);
        }
    }
}
