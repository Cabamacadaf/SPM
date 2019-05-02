using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] Spawner[] spawners;
    private bool entered = false;

    private void OnTriggerEnter (Collider other)
    {
        if (!entered && other.CompareTag("Player")) {
            entered = true;
            SpawnTriggerEvent spawnTriggerEvent = new SpawnTriggerEvent(spawners);
            spawnTriggerEvent.ExecuteEvent();
        }
    }
}
