﻿//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleObjectiveController : Singleton<FinalPuzzleObjectiveController>
{
    [SerializeField] private GameObject lights;

    public bool FinalPuzzleComplete { get; private set; }
    public bool IsTimerStarted { get; private set; }

    private int finalPuzzleCounter = 0;

    [SerializeField] private int cubesToCollect = 4;

    [SerializeField] private int[] collectionOrder = { 4, 7, 6, 2 };
    

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
            }
        }

        else {
            finalPuzzleCounter = 0;
            FinalPuzzleFailEvent finalPuzzleFailEvent = new FinalPuzzleFailEvent();
            finalPuzzleFailEvent.ExecuteEvent();
        }
    }
}
