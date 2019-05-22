﻿//Author: Marcus Mellström

using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    [SerializeField] private Spawner[] spawners;
    private bool hasBeenEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasBeenEntered && other.CompareTag("Player"))
        {
            Debug.Log("Triggered " + gameObject);
            hasBeenEntered = true;
            Spawn();
        }
    }

    private void Spawn()
    {
        foreach (Spawner spawner in spawners)
        {
            if (spawner is EnemySpawner)
            {
                EnemySpawner enemySpawner = (EnemySpawner)spawner;
                enemySpawner.Spawn();
            }
            else
            {
                spawner.Spawn();
            }
        }
    }
}
