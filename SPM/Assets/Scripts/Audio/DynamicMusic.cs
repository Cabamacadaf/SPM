using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMusic : MonoBehaviour
{
    private AudioSource[] sources;
    private AudioSource source2;
    private string colliderMusic;
    //private double time;
    //private float filterTime;

    public AudioClip[] bgMusic;

    // Start is called before the first frame update
    void Start()
    {
        sources = GetComponents<AudioSource>();
        source2 = sources[1];
        //time = AudioSettings.dspTime;
        //filterTime = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (source2.isPlaying)
        {
            return;
        }

        if (!source2.isPlaying)
        {
            MusicColliderType act = collision.gameObject.GetComponent<Collider>().gameObject.GetComponent<MusicColliderType>();
            if (act)
            {
                colliderMusic = act.GetMusicType();
                PlayDynamicMusic();
            }
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (source2.isPlaying)
        {
            source2.Stop();
            Debug.Log("Varför Stannade jag?" + colliderMusic);
            return;
        }
    }

    void PlayDynamicMusic()
    {
        // Evnetuellt en if sats som kollar att inget ljud spelar
        if (source2.isPlaying)
        {
            Debug.Log("returnade, DynamicMusic");
            return;
        }

        switch (colliderMusic)
        {
            case "Intro":
                source2.PlayOneShot(bgMusic[1]);
                Debug.Log("det triggades!");
                break;
            case "House":
                source2.PlayOneShot(bgMusic[2]);
                break;
            case "Ending":
                source2.PlayOneShot(bgMusic[3]);
                break;
            default:
                source2.PlayOneShot(bgMusic[0]);
                break;
        }
    }
}
