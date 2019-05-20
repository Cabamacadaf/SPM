using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleTimer : MonoBehaviour
{
    [SerializeField] private float objectiveTimer = 180.0f;
    [SerializeField] private TextMesh text;

    int minutes;
    int seconds;
    private string textToSet;

    private void Update ()
    {
        if (FinalPuzzleObjectiveController.Instance.IsTimerStarted) {
            objectiveTimer -= Time.deltaTime;

            SetTimerText();

            if(objectiveTimer <= 0.0f) {

            }
        }
    }

    private void SetTimerText ()
    {
        textToSet = "";
        minutes = (int)objectiveTimer / 60;
        seconds = (int)objectiveTimer % 60;
        if (minutes < 10) {
            textToSet += "0" + minutes;
        }
        else {
            textToSet += minutes;
        }
        textToSet += ":";
        if (seconds < 10) {
            textToSet += "0" + seconds;
        }
        else {
            textToSet += seconds;
        }
        text.text = textToSet;
    }
}
