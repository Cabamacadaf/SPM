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

    private BoxCollider boxCollider;
    private PlayAudioMessage playAudioMessage;

    private bool hasTriggered = false;

    protected override void Awake ()
    {
        boxCollider = GetComponentInChildren<BoxCollider>();
        playAudioMessage = GetComponent<PlayAudioMessage>();
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive && hasTriggered == false) {
            hasTriggered = true;
            InteractText.enabled = false;
            boxCollider.enabled = false;
            playAudioMessage.PlayMessage(audioLogClip, audioLogText, delayBeforeStartPlaying, timeBetweenText);
        }
    }
}
