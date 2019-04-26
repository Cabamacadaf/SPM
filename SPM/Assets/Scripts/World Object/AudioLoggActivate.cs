using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoggActivate : MonoBehaviour
{
    private AudioSource source;

    public AudioClip loggClip;
    
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !source.isPlaying) // ska ha keypress down
        {
            source.PlayOneShot(loggClip, 1.0f);
        }
    }
}
