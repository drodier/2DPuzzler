using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflector : MonoBehaviour
{
    private bool isLaserTouching = false;
    private float laserLength = 0;
    private LineRenderer laser;

    void Start()
    {
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isLaserTouching)
        {
            laser.SetPosition(0, transform.position + new Vector3(0, 0, 2));
            laser.SetPosition(1, transform.position + (transform.up * laserLength) + new Vector3(0, 0, 2));

            foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.position, transform.up))
            {
                if(hit.transform.tag == "Receiver" && Vector2.Distance(transform.position, hit.transform.position) <= laserLength)
                {
                    hit.transform.GetComponent<PuzzleSolver>().SolvePuzzle();
                }
            }
        }
        else
        {
            laser.SetPosition(0, new Vector2(0,0));
            laser.SetPosition(1, new Vector2(0,0));
        }
    }

    public void SetLaserTouching(bool status, float distanceToGo)
    {
        isLaserTouching = status;
        laserLength = distanceToGo;
    }
}
