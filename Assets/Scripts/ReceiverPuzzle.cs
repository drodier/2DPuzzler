using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiverPuzzle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer plateRenderer;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(GetComponent<PuzzleSolver>().GetStatus())
        {
            plateRenderer.color = Color.green;
        }
        else
        {
            plateRenderer.color = Color.red;
        }
    }
}
