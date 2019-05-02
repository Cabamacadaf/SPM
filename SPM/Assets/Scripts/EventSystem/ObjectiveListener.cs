using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveListener : MonoBehaviour
{
    private void Start ()
    {
        PowerCorePlacedEvent.RegisterListener(OnPowerCorePlaced);
        KeycardPickedUpEvent.RegisterListener(OnKeycardPickedUp);
    }

    void OnPowerCorePlaced(PowerCorePlacedEvent powerCorePlacedEvent)
    {
        GameController.gameControllerInstance.powerCoreCollection++;
    }

    void OnKeycardPickedUp(KeycardPickedUpEvent keycardPickedUpEvent)
    {
        GameController.gameControllerInstance.hasKeycard = true;
    }
}
