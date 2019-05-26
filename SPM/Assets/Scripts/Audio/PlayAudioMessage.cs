//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PlayAudioMessage : MonoBehaviour
{
    private AudioClip audioClip;

    private string[] subtitles;
    private float delayBeforeStartPlaying;
    private float timeBetweenText;

    private AudioSource audioSource;

    private Text subtitleText;

    protected bool StartPlaying { get; private set; }
    protected bool HasStartedPlaying { get; private set; }
    protected bool HasFinishedPlaying { get; private set; }

    private float playTimer = 0.0f;
    private int currentSubtitle;

    private void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
        subtitleText = GameManager.CanvasInstance.transform.Find("Subtitle Text").GetComponent<Text>();

        StartPlaying = false;
        HasStartedPlaying = false;
        HasFinishedPlaying = false;
    }

    protected virtual void Update ()
    {
        playTimer += Time.deltaTime;

        if (playTimer >= delayBeforeStartPlaying && HasStartedPlaying == false) {
            HasStartedPlaying = true;
            HasFinishedPlaying = false;
            audioSource.PlayOneShot(audioClip);
            subtitleText.enabled = true;
            if (currentSubtitle < subtitles.Length) {
                NextMessageText();
            }
        }

        if (playTimer >= timeBetweenText && HasStartedPlaying == true) {
            if (currentSubtitle < subtitles.Length) {
                NextMessageText();
            }
            else {
                ResetAudioMessage();
                HasFinishedPlaying = true;
            }
        }
    }

    private void NextMessageText ()
    {
        subtitleText.text = subtitles[currentSubtitle];
        currentSubtitle++;
        playTimer = 0.0f;
    }

    private void ResetAudioMessage ()
    {
        subtitleText.text = "";
        subtitleText.enabled = false;
        currentSubtitle = 0;

        StartPlaying = false;
        HasFinishedPlaying = false;
        HasStartedPlaying = false;

        audioSource.Stop();
        playTimer = 0.0f;
    }

    public void PlayMessage (AudioClip audioClip, string[] subtitles, float delayBeforeStartPlaying, float timeBetweenText)
    {
        this.audioClip = audioClip;
        this.subtitles = subtitles;
        this.delayBeforeStartPlaying = delayBeforeStartPlaying;
        this.timeBetweenText = timeBetweenText;
        ResetAudioMessage();
        StartPlaying = true;
    }
}
