using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadButtonController : MonoBehaviour
{
    public KeypadController keypad;
    public Material defaultMat;
    public Material hoveredMat;

    void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(0))
        {
            keypad.InputNumber(gameObject.name);
        }
    }

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material = hoveredMat;
    }

    void OnMouseExit()
    {
        GetComponent<Renderer>().material = defaultMat;
    }
}
