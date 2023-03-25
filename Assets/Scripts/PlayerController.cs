using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float jumpHeight = 2f;
    public float mouseSensitivity = 2f;

    private float cameraVerticalRotation = 0f;
    private float cameraHorizontalRotation = 0f;
    private bool running = false;
    public bool grounded = true;
    public bool jump = false;

    void OnCollisionEnter(Collision collision)
    {
        grounded = collision.gameObject.name == "Terrain" ? true : grounded;
    }

    void OnCollisionExit(Collision collision)
    {
        grounded = collision.gameObject.name == "Terrain" ? false : grounded;
    }

    void Update()
    {
        if(grounded && !jump && Input.GetKeyDown(KeyCode.Space))
            jump = true;

        running = Input.GetKey(KeyCode.LeftShift);

        Cursor.lockState = CursorLockMode.Locked;
        float inputX = Input.GetAxis("Mouse X")*mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*mouseSensitivity;

        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        GetComponentInChildren<Camera>().transform.localEulerAngles = Vector3.right*cameraVerticalRotation;

        cameraHorizontalRotation += inputX;
        transform.localEulerAngles = Vector3.up*cameraHorizontalRotation;
    }

    void FixedUpdate()
    {
        transform.position += transform.forward * Input.GetAxis("Vertical") * playerSpeed * (running ? 2 : 1) * Time.deltaTime;
        transform.position += transform.right * Input.GetAxis("Horizontal") * playerSpeed * (running ? 2 : 1) * Time.deltaTime;

        if(jump)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpHeight, 0));
            jump = false;
        }
    }
}
