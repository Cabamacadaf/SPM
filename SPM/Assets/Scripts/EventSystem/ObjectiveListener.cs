//Author: Marcus Mellström

using UnityEngine;

public class ObjectiveListener : MonoBehaviour
{
    private void Start ()
    {
        PowerCorePlacedEvent.RegisterListener(OnPowerCorePlaced);
        KeycardPickedUpEvent.RegisterListener(OnKeycardPickedUp);
    }

    private void OnPowerCorePlaced(PowerCorePlacedEvent powerCorePlacedEvent)
    {
        ObjectiveController.Instance.AddPowerCore();
    }

    private void OnKeycardPickedUp(KeycardPickedUpEvent keycardPickedUpEvent)
    {
        ObjectiveController.Instance.HasPowerCoreKeycard = true;
        GameManager.instance.HasKeycard = true;
    }
}
