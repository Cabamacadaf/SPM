using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLine : PlayAudioMessage
{
    protected override void Update ()
    {
        if (StartPlaying == true && HasFinishedPlaying == false) {
            base.Update();
        }
    }
}
