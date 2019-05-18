//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private float minFlickerSpeed = 0.1f;
    [SerializeField] private float maxFlickerSpeed = 0.5f;

    new private Light light;

    private float flickerTimer = 0.0f;
    private float flickerTime;

    private void Awake ()
    {
        light = GetComponent<Light>();
        flickerTime = Random.Range(minFlickerSpeed, maxFlickerSpeed);
    }

    private void Update ()
    {
        if(flickerTimer >= flickerTime) {
            if(light.enabled == true) {
                light.enabled = false;
            }
            else {
                light.enabled = true;
            }
            flickerTimer = 0;
        }
    }
}
