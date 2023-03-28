using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] private ActivatableContent activatableContent;
    
    public void TogglePuzzle()
    {
        activatableContent.TogglePuzzle();
    }

    public bool GetStatus()
    {
        return activatableContent.GetStatus();
    }
}
