//Author: Marcus Mellström

using UnityEngine;

public class FinalPuzzleDoorOpenButton : InteractiveObject
{
    [SerializeField] private Door door;

    private void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInteractive && GameController.Instance.Level2PuzzleComplete) {
            if (door.IsClosed) {
                door.Open();
            }
            else if (door.IsOpen) {
                door.Close();
            }
        }
    }
}
