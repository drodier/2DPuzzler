using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableContent : MonoBehaviour
{
    private bool solved = false;

    public void TogglePuzzle()
    {
        solved = !solved;
    }

    public bool GetStatus()
    {
        return solved; 
    }
}
