//Author: Marcus Mellström

using UnityEngine;

public class KeycardPickup : InteractiveObject
{
    private void Update ()
    {
        if(Input.GetKeyDown(KeyCode.E) && IsInteractive) {
            KeycardPickedUpEvent keycardPickedUpEvent = new KeycardPickedUpEvent(gameObject);
            keycardPickedUpEvent.EventDescription = "Keycard picked up";
            keycardPickedUpEvent.ExecuteEvent();
        }
    }
}
