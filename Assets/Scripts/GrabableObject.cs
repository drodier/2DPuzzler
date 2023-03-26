using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    [SerializeField] private int pickupLockout = 10;
    private bool tryGrab = false;
    private int timeHeld = 0;
    private bool isLockedOut = false;
    private CharacterController player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        tryGrab = Input.GetKey(KeyCode.F);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "GrabCheck" && tryGrab)
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
}
