using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayVoiceLineOnPlayerStayTrigger : MonoBehaviour
{
    [Header("Voice line properties")]
    [Tooltip("The voice line to play.")]
    [SerializeField] private AudioClip voiceLine;
    [Tooltip("The subtitles for the voice line.")]
    [SerializeField] private string[] voiceLineText;
    [Tooltip("The delay before the voice line starts playing after being triggered (Set to 0 if you want it to start playing immediately)")]
    [SerializeField] private float delayBeforeStartPlaying = 0.5f;
    [Tooltip("How long the subtitle stays on the screen before showing the next one.")]
    [SerializeField] private float timeBetweenText = 2.0f;
    [Header("Trigger properties")]
    [Tooltip("How long player has to stay in the area before the voice line plays (in seconds)")]
    [SerializeField] private float stayTimeBeforeVoiceLinePlays = 180f;

    private Player player;

    private float playerStayTimer = 0.0f;
    private bool playerIsInArea = false;

    private bool hasTriggered;

    private void Update ()
    {
        if (playerIsInArea && hasTriggered == false) {
            playerStayTimer += Time.deltaTime;
            if (playerStayTimer >= stayTimeBeforeVoiceLinePlays ) {
                hasTriggered = true;
                player.PlayVoiceLine.PlayMessage(voiceLine, voiceLineText, delayBeforeStartPlaying, timeBetweenText);
            }
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            if (player == null) {
                player = other.GetComponent<Player>();
            }
            playerIsInArea = true;
        }
    }

    private void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Player")) {
            playerIsInArea = false;
            playerStayTimer = 0.0f;
        }
    }
}
