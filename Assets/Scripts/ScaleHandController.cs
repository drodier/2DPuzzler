using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleHandController : MonoBehaviour
{
    [SerializeField] private GrabableObject heldItem;

    [SerializeField]private int tryAction = 0;

    void FixedUpdate()
    {
        if(heldItem != null)
        {
            heldItem.transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f + heldItem.transform.localScale.y/5, transform.position.z);
            heldItem.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;

            transform.localPosition = new Vector3(transform.localPosition.x, -((float)heldItem.GetWeight())/10f, transform.localPosition.z);
        }
        else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
        }

        tryAction -= tryAction > 0 ? 1 : 0;
    }

    void Update()
    {
        tryAction = Input.GetKeyUp(KeyCode.E) ? 10 : tryAction;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Player" && tryAction > 0)
        {
            CharacterController player = other.GetComponent<CharacterController>();
            
            if(player.IsHolding())
            {
                heldItem = player.DropItem();
            }
            else if(heldItem != null)
            {
                GrabableObject temp = heldItem;
                heldItem = null;
                player.PickUpItem(temp);
            }

            tryAction = 0;
        }

    }
}
