using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandController : MonoBehaviour
{
    [SerializeField] private GrabableObject heldItem;
    [SerializeField] private SpriteRenderer childRenderer;

    void FixedUpdate()
    {
        if(heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y/2) + (heldItem.GetComponent<SpriteRenderer>().bounds.size.y/2), transform.position.z);
            heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        } 
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CharacterController player = other.GetComponent<CharacterController>();

            if(player.IsHolding())
            {
                childRenderer.enabled = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            childRenderer.enabled = false;
        }
    }

    public void PlaceObject(GrabableObject other)
    {
        heldItem = other;
        childRenderer.enabled = false;
    }

    public GrabableObject DropObject()
    {
        GrabableObject tmp = heldItem;
        heldItem = null;
        childRenderer.enabled = true;

        return tmp;
    }

    public int GetObjectWeight()
    {
        if(heldItem != null)
            return heldItem.GetWeight();

        return 0;
    }
}
