using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleTimer : MonoBehaviour
{
    [SerializeField] private float objectiveTimer = 180.0f;
    [SerializeField] private TextMesh text;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip alarmSound;

    private int minutes;
    private int seconds;
    private string textToSet;
    private bool hasStarted = false;

    private void Update ()
    {
        if (FinalPuzzleObjectiveController.Instance.IsTimerStarted) {
            if (hasStarted == false) {
                hasStarted = true;
                audioSource.PlayOneShot(alarmSound);
            }

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
