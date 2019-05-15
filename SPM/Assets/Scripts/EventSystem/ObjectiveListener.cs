//Author: Marcus Mellström

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
        GameController.Instance.AddPowerCore();
    }

    void OnKeycardPickedUp(KeycardPickedUpEvent keycardPickedUpEvent)
    {
        GameController.Instance.HasLevel1Keycard = true;
    }
}
