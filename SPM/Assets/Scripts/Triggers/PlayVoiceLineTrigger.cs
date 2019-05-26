//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayVoiceLineTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip voiceLine;

    [SerializeField] private string[] voiceLineText;
    [SerializeField] private float delayBeforeStartPlaying = 0.5f;
    [SerializeField] private float timeBetweenText = 2.0f;

    private Text subtitleText;

    private bool hasTriggered = false;

    private void Awake ()
    {
        subtitleText = GameManager.CanvasInstance.transform.Find("Voice Line Subtitle Text").GetComponent<Text>();
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && hasTriggered == false) {
            hasTriggered = true;
            GameManager.PlayerInstance.PlayVoiceLine.PlayMessage(voiceLine, subtitleText, voiceLineText, delayBeforeStartPlaying, timeBetweenText);
        }
    }
}
