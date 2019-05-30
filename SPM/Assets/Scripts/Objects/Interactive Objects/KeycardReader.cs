//Author: Marcus Mellström

using UnityEngine;

public class KeycardReader : InteractiveObject
{
    [SerializeField] private new Light light;
    [SerializeField] private Color onColor;
    [SerializeField] private Door door;

    private bool lightChanged = false;

    protected override void Awake ()
    {
        TextToSet = "You need a Keycard to open this door";
        base.Awake();
    }

    private void Update ()
    {
        if (!lightChanged && ObjectiveController.Instance.HasPowerCoreKeycard && ObjectiveController.Instance.HasAllPowerCores) {
            light.color = onColor;
            TextToSet = "Press E to interact";
            lightChanged = true;
        }

        if(Input.GetKeyDown(KeyCode.E) && IsInteractive && ObjectiveController.Instance.HasPowerCoreKeycard && ObjectiveController.Instance.HasAllPowerCores) {
            if (door.IsClosed) {
                door.Open();
            }
            else if (door.IsOpen) {
                door.Close();
            }
        }
    }
}
