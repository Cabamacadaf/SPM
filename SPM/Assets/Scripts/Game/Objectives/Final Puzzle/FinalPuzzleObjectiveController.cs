//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleObjectiveController : Singleton<FinalPuzzleObjectiveController>
{
    [SerializeField] private GameObject lights;
    [SerializeField] private Door door;

    public bool FinalPuzzleComplete { get; private set; }
    public bool IsTimerStarted { get; private set; }

    private int finalPuzzleCounter = 0;

    [SerializeField] private int cubesToCollect = 4;

    [SerializeField] private int[] collectionOrder = { 6, 2, 5, 7 };
    

    private void Awake ()
    {
        FinalPuzzleComplete = false;
    }

    public void TryToAddPowerCube (int id)
    {
        if (id == collectionOrder[finalPuzzleCounter]) {
            if(IsTimerStarted == false) {
                IsTimerStarted = true;
            }
            finalPuzzleCounter++;
            if (finalPuzzleCounter >= cubesToCollect) {
                FinalPuzzleComplete = true;
                lights.SetActive(true);
                door.Open();
            }
        }

        else {
            finalPuzzleCounter = 0;
            FinalPuzzleFailEvent finalPuzzleFailEvent = new FinalPuzzleFailEvent();
            finalPuzzleFailEvent.ExecuteEvent();
        }
    }
}
