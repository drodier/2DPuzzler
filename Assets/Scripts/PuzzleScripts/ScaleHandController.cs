using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandController : MonoBehaviour
{
    [SerializeField] private Stack<GrabableObject> heldItems;
    [SerializeField] private SpriteRenderer childRenderer;

    private bool isLocked = false;

    void FixedUpdate()
    {
        if(heldItems.Count > 0)
        {
            for(int i=0; i<heldItems.Count; i++)
            {
                heldItems.ToArray()[i].transform.position = new Vector3(
                            (transform.position.x + (heldItems.Count % 2 != 0 && i == heldItems.Count-1 ? 0 : heldItems.ToArray()[i].GetComponent<SpriteRenderer>().bounds.size.x/2 * (i%2==0 ? -1.5f : 1.5f))), 
                            (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y/2) + (heldItems.ToArray()[i].GetComponent<SpriteRenderer>().bounds.size.y/2) + (i-2>0 ? heldItems.ToArray()[i].GetComponent<SpriteRenderer>().bounds.size.y * i-2 : 0), 
                            transform.position.z);

                heldItems.ToArray()[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
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
        if(other != null && !isLocked)
        {
            heldItems.Push(other);
            childRenderer.enabled = false;
        }
    }

    public GrabableObject DropObject()
    {
        GrabableObject ret = null;

        if(!isLocked)
        {
            ret = heldItems.ToArray()[heldItems.Count - 1];

            heldItems.Pop();

            childRenderer.enabled = heldItems.Count <= 0;
        }

        return ret;
    }

    public int GetObjectWeight()
    {
        int weight = 0;

        for(int i=0; i<heldItems.Count; i++)
        {
            weight += heldItems.ToArray()[i].GetWeight();
        }

        return weight;
    }

    public void ChangeLock(bool newStatus)
    {
        isLocked = newStatus;
    }
}
