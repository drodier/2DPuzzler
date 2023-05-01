using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandController : MonoBehaviour
{
    [SerializeField] private GrabableObject heldItem;
    [SerializeField] private SpriteRenderer childRenderer;

    private int tryAction = 0;

    void FixedUpdate()
    {
        if(heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, (transform.position.y + GetComponent<SpriteRenderer>().bounds.size.y/2) + (heldItem.GetComponent<SpriteRenderer>().bounds.size.y/2), transform.position.z);
            heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

        tryAction -= tryAction > 0 ? 1 : 0;
    }

    void Update()
    {
        tryAction = Input.GetKeyUp(KeyCode.E) ? 10 : tryAction;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CharacterController player = other.GetComponent<CharacterController>();

            if(tryAction > 0)
            {
                if(player.IsHolding())
                {
                    heldItem = player.DropItem();
                    childRenderer.enabled = false;
                }
                else if(heldItem != null)
                {
                    GrabableObject temp = heldItem;
                    heldItem = null;
                    player.PickUpItem(temp);
                    childRenderer.enabled = true;
                }

                tryAction = 0;
            }

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

    public int GetObjectWeight()
    {
        if(heldItem != null)
            return heldItem.GetWeight();

        return 0;
    }
}
