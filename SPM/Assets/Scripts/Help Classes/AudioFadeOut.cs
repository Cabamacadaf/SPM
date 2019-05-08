//Author: Marcus Mellström

using System.Collections;
using UnityEngine;

public class AudioFadeOut : MonoBehaviour
{
    public static AudioFadeOut instance;

    public void StartCoroutine (AudioSource audioSource, float fadeTime)
    {
        StartCoroutine(FadeOut(audioSource, fadeTime));
    }

    public IEnumerator FadeOut (AudioSource audioSource, float fadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0) {
            audioSource.volume -= startVolume * Time.deltaTime / fadeTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }
}
