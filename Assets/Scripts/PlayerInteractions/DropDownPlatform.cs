using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownPlatform : MonoBehaviour
{
    private bool isHoldingDown = false;

    // Update is called once per frame
    void Update()
    {
        isHoldingDown = Input.GetAxis("Vertical") == -1;
    }

    void FixedUpdate()
    {
        PlatformEffector2D effector = GetComponent<PlatformEffector2D>();

        if(isHoldingDown)
            effector.rotationalOffset = 180;
        else
            effector.rotationalOffset = 0;
    }
}
