//Author: Marcus Mellström

using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Spawner[] spawners;
    private bool wasEntered = false;

    private void OnTriggerEnter (Collider other)
    {
        if (!wasEntered && other.CompareTag("Player")) {
            wasEntered = true;
            SpawnTriggerEvent spawnTriggerEvent = new SpawnTriggerEvent(spawners);
            spawnTriggerEvent.ExecuteEvent();
        }
    }
}
