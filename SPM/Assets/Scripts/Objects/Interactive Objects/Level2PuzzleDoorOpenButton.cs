//Author: Marcus Mellström

using UnityEngine;

public class Level2PuzzleDoorOpenButton : InteractiveObject
{
    [SerializeField] private Door door;

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactive && GameController.Instance.Level2PuzzleComplete) {
            if (door.closed) {
                door.Open();
            }
            else if (door.open) {
                door.Close();
            }
        }
    }
}
