using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            KeycardPickedUpEvent keycardPickedUpEvent = new KeycardPickedUpEvent(gameObject);
            keycardPickedUpEvent.eventDescription = "Keycard picked up";
            keycardPickedUpEvent.ExecuteEvent();
        }
    }
}
