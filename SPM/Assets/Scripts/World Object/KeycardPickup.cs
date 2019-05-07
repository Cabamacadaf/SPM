using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeycardPickup : InteractiveObject
{
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && interactive) {
            KeycardPickedUpEvent keycardPickedUpEvent = new KeycardPickedUpEvent(gameObject);
            keycardPickedUpEvent.eventDescription = "Keycard picked up";
            keycardPickedUpEvent.ExecuteEvent();
        }
    }
}
