using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : InteractiveObject
{
    [SerializeField] private Light[] lights;
    public Light ButtonLight;

    private void Start ()
    {
        ButtonLight.enabled = true;
        foreach (Light light in lights) {
            light.enabled = false;
        }
    }
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive) {
            TurnOnLights();
            ButtonLight.enabled = false;
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pick Up Objects")) {
            TurnOnLights();
            ButtonLight.enabled = false;
        }
    }

    private void TurnOnLights ()
    {
        foreach (Light light in lights) {
            light.enabled = true;
        }
    }
}
