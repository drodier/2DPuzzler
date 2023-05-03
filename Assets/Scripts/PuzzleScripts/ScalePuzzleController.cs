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

        Vector3 leftDesiredPosition = leftDefaultPosition + new Vector3(0, (float)value/10, 0);
        leftHand.transform.position = Vector3.MoveTowards(leftHand.transform.position, leftDesiredPosition, 0.01f);
        Vector3 rightDesiredPosition = rightDefaultPosition - new Vector3(0, (float)value/10, 0);
        rightHand.transform.position = Vector3.MoveTowards(rightHand.transform.position, rightDesiredPosition, 0.01f);

        leftHand.ChangeLock(Vector3.Distance(leftHand.transform.position, leftDesiredPosition) >= 0.05f);
        rightHand.ChangeLock(Vector3.Distance(rightHand.transform.position, rightDesiredPosition) >= 0.05f);
    }

}
