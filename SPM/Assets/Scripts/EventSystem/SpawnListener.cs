//Author: Marcus Mellström

using UnityEngine;

public class SpawnListener : MonoBehaviour
{
    private void Start()
    {
        SpawnTriggerEvent.RegisterListener(OnSpawnTrigger);
    }

    private void OnSpawnTrigger (SpawnTriggerEvent spawnTriggerEvent)
    {
        //want to avoid foreach, don't know if possible
        foreach (Spawner spawner in spawnTriggerEvent.spawners) {
            if (spawner is EnemySpawner) {
                EnemySpawner enemySpawner = (EnemySpawner)spawner;
                enemySpawner.Spawn();
            }
            else {
                spawner.Spawn();
            }
        }
    }
}
