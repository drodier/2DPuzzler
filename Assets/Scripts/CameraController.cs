using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float maxCameraSpeed = 3;
    [SerializeField] private float camSpeed = 3;

    private Vector2 desiredPosition;

    // Update is called once per frame
    void Update()
    {
        desiredPosition = player.transform.position + new Vector3(player.transform.localScale.x, .5f, 0);
    }

    void FixedUpdate()
    {
        Vector2 direction = Vector2.ClampMagnitude(desiredPosition - new Vector2(transform.position.x, transform.position.y), maxCameraSpeed) * camSpeed * Time.deltaTime;
        transform.position += new Vector3(direction.x, direction.y, 0);
    }
}
