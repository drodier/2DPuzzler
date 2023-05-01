using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    [SerializeField] private int pickupLockout = 10;
    [SerializeField] private int weight = 1;
    [SerializeField] public float throwForce = 10f;
    [SerializeField] private string facingDirection = "Up";
    [SerializeField] private float rotationSpeed = 3;
    private int timeHeld = 0;
    private bool isLockedOut = false;
    private CharacterController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "GrabCheck" && Input.GetButton("Interact"))
        {
            player.PickUpItem(this);
        }
    }

    void FixedUpdate()
    {
        if(isLockedOut)
        {
            if(timeHeld <= pickupLockout)
            {
                timeHeld++;
            }
            else
            {
                timeHeld = 0;
                isLockedOut = false;
            }
        }

        TurnAnimation();
    }

    public void StartLockout()
    {
        isLockedOut = true;
        timeHeld = 0;
    }

    public bool IsLockedOut()
    {
        return isLockedOut;
    }

    public int GetWeight()
    {
        return weight;
    }

    public void ChangeWeight(int modification)
    {
        if(weight + modification < 1)
            weight = 1;
        else
            weight += modification;
    }

    public void ThrowItem()
    {
        Vector2 heading = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        float distance = heading.magnitude;
        Vector2 throwDirection = heading / distance;

        player.DropItem();
        throwDirection *= GetWeightMultiplier();

        GetComponent<Rigidbody2D>().velocity = throwDirection * throwForce;
    }

    public float GetWeightMultiplier()
    {
        // Calculate weight multiplier based on weight value
        return Mathf.Clamp(1 - (weight / 10f), 0.5f, 1f);
    }

    public void RotateObject()
    {
        if(facingDirection == "Up")
        {
            facingDirection = "Right";
        }
        else if(facingDirection == "Right")
        {
            facingDirection = "Down";
        }
        else if(facingDirection == "Down")
        {
            facingDirection = "Left";
        }
        else if(facingDirection == "Left")
        {
            facingDirection = "Up";
        }
    }

    private void TurnAnimation()
    {
        bool lockAnimation = false;
        float zRotation = (transform.eulerAngles).z;

        if(facingDirection == "Up")
        {
            lockAnimation = zRotation < 5 || zRotation < -5;

            if(lockAnimation)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,0));
        }
        else if(facingDirection == "Right")
        {
            lockAnimation = zRotation > 265 && zRotation < 275;

            if(lockAnimation)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,-90));
        }
        else if(facingDirection == "Down")
        {
            lockAnimation = zRotation > 175 && zRotation < 185;

            if(lockAnimation)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,-180));
        }
        else if(facingDirection == "Left")
        {
            lockAnimation = zRotation > 85 && zRotation < 95;

            if(lockAnimation)
                transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,0,90));
        }

        if(!lockAnimation)
            transform.Rotate(Vector3.forward, -rotationSpeed);
    }
}
