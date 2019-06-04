using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public int reachedCheckpoint;
    public int ID;

    public CheckpointData(CheckpointTrigger checkpoint)
    {
        ID = checkpoint.ID;
        if (checkpoint.reachedCheckpoint)
        {
            reachedCheckpoint = 1;
        }
        else
        {
            reachedCheckpoint = 0;
        }
    }
}
