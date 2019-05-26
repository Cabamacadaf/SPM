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

    protected override void Awake ()
    {
        playAudioLogMessage = GetComponent<PlayAudioLogMessage>();
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive) {
            playAudioLogMessage.PlayMessage(audioLogClip, audioLogText, delayBeforeStartPlaying, timeBetweenText);
        }
    }
}
