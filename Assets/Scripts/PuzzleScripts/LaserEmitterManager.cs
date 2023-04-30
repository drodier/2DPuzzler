using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEmitterManager : MonoBehaviour
{
    [SerializeField] private bool isActivated = true;
    [SerializeField] private float maxDistance = 10;
    [SerializeField] private AudioClip laserSound;

    private Vector3 target;
    private LineRenderer laser;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        laser = GetComponent<LineRenderer>();
        laser.SetPosition(0, transform.position + new Vector3(0, 0, 2));
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = laserSound;
        audioSource.loop = true; // make sure the audio source is set to loop
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(Reflector reflector in GameObject.FindObjectsOfType<Reflector>())
        {
            reflector.SetLaserTouching(false, 0);
        }
        
        if(isActivated)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);

            if(hit.collider != null)
            {
                if(hit.distance <= maxDistance && hit.transform.tag != "TileMap")
                {
                    target = new Vector2(hit.transform.position.x, transform.position.y);

                    if(hit.transform.tag == "Reflective")
                    {
                        hit.transform.GetComponent<Reflector>().SetLaserTouching(true, GetDistanceToGo());
                    }
                }
                else
                    target = new Vector2(transform.position.x + maxDistance, transform.position.y);
            }
            else
            {
                target = new Vector2(transform.position.x + maxDistance, transform.position.y);
            }

            laser.SetPosition(1, target + new Vector3(0, 0, 2));

            // calculate the distance between the player and the laser
            float distanceToPlayer = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

            // set the volume of the audio source based on the distance to the player
            audioSource.volume = 0.8f / (distanceToPlayer * 15);

            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            if(audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    float GetDistanceToGo()
    {
        return maxDistance - (Vector2.Distance(transform.position, target));
    }
}
