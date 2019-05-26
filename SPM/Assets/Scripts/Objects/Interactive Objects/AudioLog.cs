//Author: Marcus Mellström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioLog : InteractiveObject
{
    [SerializeField] private AudioClip audioLogClip;

    [SerializeField] private string[] audioLogText;
    [SerializeField] private float delayBeforeStartPlaying = 0.5f;
    [SerializeField] private float timeBetweenText = 2.0f;
    
    private PlayAudioMessage playAudioLogMessage;
    private Text subtitleText;

    protected override void Awake ()
    {
        playAudioLogMessage = GetComponent<PlayAudioLogMessage>();
        subtitleText = GameManager.CanvasInstance.transform.Find("Voice Line Subtitle Text").GetComponent<Text>();
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive) {
            playAudioLogMessage.PlayMessage(audioLogClip, subtitleText, audioLogText, delayBeforeStartPlaying, timeBetweenText);
        }
    }
}
