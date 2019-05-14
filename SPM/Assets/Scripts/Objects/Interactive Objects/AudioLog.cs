//Author: Marcus Mellström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AudioLog : InteractiveObject
{
    [SerializeField] private string[] audioLogText;
    [SerializeField] private float delayBeforeStartPlaying = 0.5f;
    [SerializeField] private float timeBetweenText = 2.0f;
    private AudioSource audioSource;

    [SerializeField] private AudioClip audioLogClip;

    private Text messageText;

    private bool played = false;
    
    private new void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        messageText = FindObjectOfType<Canvas>().transform.Find("Message Text").GetComponent<Text>();
        base.Awake();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactive && !played) {
            played = true;
            interactText.enabled = false;
            GetComponentInChildren<BoxCollider>().enabled = false;
            StartCoroutine(PlayAudioLogMessage());
        }
    }

    private IEnumerator PlayAudioLogMessage ()
    {
        yield return new WaitForSeconds(delayBeforeStartPlaying);
        audioSource.PlayOneShot(audioLogClip);
        messageText.enabled = true;
        foreach (string text in audioLogText){
            messageText.text = text;
            yield return new WaitForSeconds(timeBetweenText);
        }
        messageText.text = "";
        messageText.enabled = false;
    }
}
