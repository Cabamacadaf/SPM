//Author: Marcus Mellström

using UnityEngine;

public class DoorOpenButton : InteractiveObject
{
    [SerializeField] private Door door;

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive) {
            if (door.IsClosed) {
                door.Open();
            }
            else if (door.IsOpen) {
                door.Close();
            }
        }
    }
}
