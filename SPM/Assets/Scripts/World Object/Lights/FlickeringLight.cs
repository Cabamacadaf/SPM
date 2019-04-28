using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private float minFlickerSpeed = 0.1f;
    [SerializeField] private float maxFlickerSpeed = 0.5f;

    new private Light light;
    private bool running = false;

    private void Awake ()
    {
        light = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    private void Update ()
    {
        if (!running) {
            StartCoroutine(Flicker());
        }
    }

    private IEnumerator Flicker()
    {
        running = true;
        light.enabled = true;
        yield return new WaitForSeconds (Random.Range(minFlickerSpeed, maxFlickerSpeed));
        light.enabled = false;
        yield return new WaitForSeconds(Random.Range(minFlickerSpeed, maxFlickerSpeed));
        running = false;
    }
}
