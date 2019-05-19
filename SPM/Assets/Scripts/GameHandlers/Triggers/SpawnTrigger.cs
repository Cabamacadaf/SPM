//Author: Marcus Mellström

using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Spawner[] spawners;
    private bool hasBeenEntered = false;

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log("Fisk");
        if (!hasBeenEntered && other.CompareTag("Player")) {
            hasBeenEntered = true;
            SpawnTriggerEvent spawnTriggerEvent = new SpawnTriggerEvent(spawners);
            spawnTriggerEvent.ExecuteEvent();
        }
    }
}
