using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabableObject : MonoBehaviour
{
    [SerializeField] private int pickupLockout = 10;
    [SerializeField] private int weight = 1;
    private int timeHeld = 0;
    private bool isLockedOut = false;
    private CharacterController player;

    public float GetWeightMultiplier()
    {
        // Calculate weight multiplier based on weight value
        return Mathf.Clamp(1 - (weight / 10f), 0.5f, 1f);
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<CharacterController>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.name == "GrabCheck" && Input.GetKey(KeyCode.F))
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
}
