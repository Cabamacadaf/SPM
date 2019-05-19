//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleObjectiveController : Singleton<FinalPuzzleObjectiveController>
{
    public bool FinalPuzzleComplete { get; private set; }

    private int finalPuzzleCounter = 0;
    private int cubesToCollect = 7;

    private int[] collectionOrder = { 4, 7, 6, 2, 5, 1, 3 };

    [SerializeField] private GameObject lights;

    private void Awake ()
    {
        FinalPuzzleComplete = false;
    }

    public void TryToAddPowerCube (int id)
    {
        if (id == collectionOrder[finalPuzzleCounter]) {
            finalPuzzleCounter++;
            if (finalPuzzleCounter >= cubesToCollect) {
                FinalPuzzleComplete = true;
                lights.SetActive(true);
            }
        }
        else {
            finalPuzzleCounter = 0;
            FinalPuzzleFailEvent finalPuzzleFailEvent = new FinalPuzzleFailEvent();
            finalPuzzleFailEvent.ExecuteEvent();
        }
    }
}
