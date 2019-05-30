using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPuzzleEnterRoomTrigger : MonoBehaviour
{
    [SerializeField] private Door[] doors;
    private void OnTriggerEnter (Collider other)
    {
        foreach (Door door in doors) {
            door.Close();
        }
    }
}
