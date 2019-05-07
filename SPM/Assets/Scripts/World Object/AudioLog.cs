using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLog : InteractiveObject
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip audioLogClip;

    private bool played = false;
    
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        base.Awake();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactive && !played) {
            played = true;
            audioSource.PlayOneShot(audioLogClip);
        }
    }
}
