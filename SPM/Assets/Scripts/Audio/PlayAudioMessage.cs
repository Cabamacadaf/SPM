using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayAudioMessage : MonoBehaviour
{
    private AudioClip audioClip;

    private string[] subtitles;
    private float delayBeforeStartPlaying;
    private float timeBetweenText;

    private AudioSource audioSource;

    private Text messageText;

    private bool startPlaying = false;
    private bool hasStartedPlaying = false;
    private bool hasFinishedPlaying = false;

    private float playTimer = 0.0f;
    private int currentSubtitle;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        messageText = FindObjectOfType<Canvas>().transform.Find("Message Text").GetComponent<Text>();
    }

    private void Update ()
    {
        if (startPlaying == true && hasFinishedPlaying == false) {
            playTimer += Time.deltaTime;

            if (playTimer >= delayBeforeStartPlaying && hasStartedPlaying == false) {
                hasStartedPlaying = true;
                audioSource.PlayOneShot(audioClip);
                messageText.enabled = true;
                NextMessageText();
            }

            if (playTimer >= timeBetweenText && hasStartedPlaying == true) {
                if (currentSubtitle < subtitles.Length) {
                    NextMessageText();
                }
                else {
                    messageText.text = "";
                    messageText.enabled = false;
                    hasFinishedPlaying = true;
                }
            }
        }
    }

    private void NextMessageText ()
    {
        messageText.text = subtitles[currentSubtitle];
        currentSubtitle++;
        playTimer = 0.0f;
    }

    public void PlayMessage (AudioClip audioClip, string[] subtitles, float delayBeforeStartPlaying, float timeBetweenText)
    {
        this.audioClip = audioClip;
        this.subtitles = subtitles;
        this.delayBeforeStartPlaying = delayBeforeStartPlaying;
        this.timeBetweenText = timeBetweenText;
        startPlaying = true;
    }
}
