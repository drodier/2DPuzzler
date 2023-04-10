using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    void OnMouseEnter()
    {
        transform.localScale += new Vector3(0.1f, 0, 0.1f);
    }

    void OnMouseExit()
    {
        transform.localScale -= new Vector3(0.1f, 0, 0.1f);
    }

    void OnMouseOver()
    {
        if(Input.GetMouseButtonUp(0))
        {
            GetComponent<PuzzleSolver>().SolvePuzzle();
        }
    }
}
