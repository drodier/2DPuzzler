using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public enum PlatformDirection { UpDown, LeftRight };  // define an enum for the platform direction
    public PlatformDirection direction;                  // serialized field for the platform direction
    public float distance = 5f;                          // serialized field for the distance the platform moves
    public float speed = 2f;                             // speed of the platform movement

    private Vector3 startPosition;                       // starting position of the platform
    private Vector3 endPosition;                         // ending position of the platform
    private float startTime;                             // time when the movement started
    private float journeyLength;

    private GameObject player;                           // reference to the player's GameObject

    void Start()
    {
        startPosition = transform.position;
        if (direction == PlatformDirection.UpDown)
        {
            endPosition = new Vector3(startPosition.x, startPosition.y + distance, startPosition.z);
        }
        else
        {
            endPosition = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
        }
        journeyLength = Vector3.Distance(startPosition, endPosition);
    }

    void Update()
    {
        if(GetComponent<ActivatableContent>().GetStatus())
        {
            float journeyTime = Time.time - startTime;
            float fracJourney = Mathf.PingPong(journeyTime * speed, journeyLength) / journeyLength;
            transform.position = Vector3.Lerp(startPosition, endPosition, fracJourney);
            if (fracJourney == 1)
            {
                if (direction == PlatformDirection.UpDown)
                {
                    endPosition = startPosition;
                    startPosition = transform.position;
                }
                else
                {
                    startPosition = endPosition;
                    endPosition = new Vector3(startPosition.x + distance, startPosition.y, startPosition.z);
                    journeyLength = Vector3.Distance(startPosition, endPosition);
                }
                startTime = Time.time;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.transform.SetParent(null);
            player = null;
        }
    }
}
