using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public TMP_Text screen;
    public string solution;

    public void InputNumber(string number)
    {
        if(number == "Clear")
        {
            screen.text = "";
            screen.color = new Color(0, 0, 0);
            if(GetComponent<PuzzleSolver>().GetStatus())
                GetComponent<PuzzleSolver>().SolvePuzzle();
        }
        else
        {
            screen.text += number;
            
            if(screen.text.Length >= 4)
                CheckSolution();
        }
    }

    void CheckSolution()
    {
        if(screen.text == solution)
        {
            GetComponent<PuzzleSolver>().SolvePuzzle();
            screen.color = new Color(0, 1, 0);
        }
        else
        {
            screen.color = new Color(1, 0, 0);
        }
    }

}
