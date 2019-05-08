using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : InteractiveObject
{
    [SerializeField] private Light[] lights;

    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive) {
            TurnOnLights();
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pick Up Objects")) {
            TurnOnLights();
        }
    }

    private void TurnOnLights ()
    {
        foreach (Light light in lights) {
            light.enabled = true;
        }
    }
}
