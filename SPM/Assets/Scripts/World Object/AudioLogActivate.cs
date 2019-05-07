using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogActivate : MonoBehaviour
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

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !source.isPlaying && Input.GetKeyDown(KeyCode.E)) // ska ha keypress down
        {
            source.PlayOneShot(loggClip, 1.0f);
        }
    }
}
