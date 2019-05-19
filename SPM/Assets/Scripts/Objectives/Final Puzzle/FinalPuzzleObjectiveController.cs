using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleObjectiveController : Singleton<FinalPuzzleObjectiveController>
{
    public bool FinalPuzzleComplete { get; private set; }

    private int finalPuzzleCounter = 0;
    private int cubesToCollect = 7;

    private void Awake ()
    {
        FinalPuzzleComplete = false;
    }

    public void AddPowerCube ()
    {
        finalPuzzleCounter++;
        if (finalPuzzleCounter >= cubesToCollect) {
            FinalPuzzleComplete = true;
        }
    }
}
