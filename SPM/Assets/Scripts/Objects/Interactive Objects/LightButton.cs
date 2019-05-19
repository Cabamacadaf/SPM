//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : InteractiveObject
{
    [SerializeField] private Light[] lights;

    [SerializeField] private Light ButtonLight;

    protected override void Awake ()
    {
        base.Awake();
        ButtonLight.enabled = true;
        foreach (Light light in lights) {
            light.enabled = false;
        }
    }

    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInteractive) {
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
