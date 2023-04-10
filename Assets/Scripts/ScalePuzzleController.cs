using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePuzzleController : MonoBehaviour
{
    [SerializeField] private GameObject[] hands;
    [SerializeField] private int[] solution;
    [SerializeField] private int[] value = {0,0,0,0};

    void FixedUpdate()
    {
        for(int i=0; i<hands.Length; i++)
        {
            value[i] = Mathf.RoundToInt(hands[i].transform.localPosition.y * -10);
        }

        if(value[0] == solution[0] && value[1] == solution[1] && value[2] == solution[2] && value[3] == solution[3] && !GetComponent<PuzzleSolver>().GetStatus())
            GetComponent<PuzzleSolver>().SolvePuzzle();
    }

}
