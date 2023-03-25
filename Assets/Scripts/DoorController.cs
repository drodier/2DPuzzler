using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float animationSpeed = 1f;

    public bool isClosed = true;
    private ActivatableContent activatableContent;
    private Vector3 closedPosition;
    private Vector3 closedScale;

    void Start()
    {
        activatableContent = GetComponent<ActivatableContent>();
        closedPosition = transform.position;
        closedScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        isClosed = !activatableContent.GetStatus();
    }

    void FixedUpdate()
    {
        if(isClosed && transform.localScale.y < closedScale.y)
        {
            if(transform.localScale.y <= animationSpeed)
                transform.position -= new Vector3(0, animationSpeed, 0);
            transform.position -= new Vector3(0, animationSpeed/2, 0);
            transform.localScale += new Vector3(0, animationSpeed, 0);
        }
        if(!isClosed && transform.localScale.y > animationSpeed)
        {
            transform.position += new Vector3(0, animationSpeed/2, 0);
            transform.localScale -= new Vector3(0, animationSpeed, 0);
            if(transform.localScale.y <= animationSpeed)
                transform.position += new Vector3(0, animationSpeed, 0);
        }
    }
}
