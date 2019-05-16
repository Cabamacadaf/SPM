﻿//Author: Marcus Mellström

using UnityEngine;

public class KeycardReader : InteractiveObject
{
    [SerializeField] private new Light light;
    [SerializeField] private Color onColor;
    private bool lightChanged = false;
    [SerializeField] private Door door;

    private new void Awake ()
    {
        textToSet = "You need a Keycard to open this door";
        base.Awake();
    }

    private void Update ()
    {
        if (!lightChanged && GameController.Instance.HasLevel1Keycard && GameController.Instance.HasAllPowerCores) {
            light.color = onColor;
            textToSet = "Press E to interact";
            lightChanged = true;
        }

        if(Input.GetKeyDown(KeyCode.E) && interactive && GameController.Instance.HasLevel1Keycard && GameController.Instance.HasAllPowerCores) {
            if (door.closed) {
                door.Open();
            }
            else if (door.open) {
                door.Close();
            }
        }
    }
}
