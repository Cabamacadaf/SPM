using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public bool reachedCheckpoint;

    public CheckpointData(CheckpointTrigger checkpoint)
    {
        reachedCheckpoint = checkpoint.reachedCheckpoint;
    }
}
