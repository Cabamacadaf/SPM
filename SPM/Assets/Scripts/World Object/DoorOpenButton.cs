//Author: Marcus Mellström

using UnityEngine;

public class DoorOpenButton : InteractiveObject
{
    [SerializeField] private Door door;

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactive) {
            if (door.closed) {
                door.Open();
            }
            else if (door.open) {
                door.Close();
            }
        }
    }
}
