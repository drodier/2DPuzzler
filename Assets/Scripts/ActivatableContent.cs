using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableContent : MonoBehaviour
{
    [SerializeField] private bool solved = false;

    public void TogglePuzzle()
    {
        solved = !solved;
    }

    public bool GetStatus()
    {
        return solved; 
    }
}
