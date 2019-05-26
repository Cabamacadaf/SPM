using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioLogMessage : PlayAudioMessage
{
    protected override void Update ()
    {
        if (StartPlaying == true) {
            base.Update();
        }
    }
}
