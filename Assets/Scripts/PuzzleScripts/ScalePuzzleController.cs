using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalePuzzleController : MonoBehaviour
{
    [SerializeField] private ScaleHandController leftHand, rightHand;
    [SerializeField] private int solution;
    [SerializeField] private int value = 0;

    private Vector3 leftDefaultPosition, rightDefaultPosition;

    void Start()
    {
        leftDefaultPosition = leftHand.transform.position;
        rightDefaultPosition = rightHand.transform.position;
    }

    void FixedUpdate()
    {
        value = rightHand.GetObjectWeight() - leftHand.GetObjectWeight();

        if(value == solution && !GetComponent<PuzzleSolver>().GetStatus())
            GetComponent<PuzzleSolver>().SolvePuzzle();

        leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, leftDefaultPosition + new Vector3(0, (float)value/10, 0), 0.01f);
        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rightDefaultPosition - new Vector3(0, (float)value/10, 0), 0.01f);
    }

}
