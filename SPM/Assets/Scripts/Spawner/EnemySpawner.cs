//Author: Marcus Mellström

using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private bool playSoundOnSpawn = false;
    [SerializeField] private bool aggroOnSpawn = false;

    public new void Spawn ()
    {
        Enemy enemyInstant = base.Spawn().GetComponent<Enemy>();
        if (aggroOnSpawn) {
            if (enemyInstant is Enemy1) {
                enemyInstant.Transition<Enemy1AggroState>();
            }
            else if(enemyInstant is Enemy2) {
                enemyInstant.Transition<Enemy2AggroState>();
            }
        }

        if (playSoundOnSpawn) {
            EnemyBaseState enemyBaseState = (EnemyBaseState)enemyInstant.GetCurrentState();
            //enemyBaseState.PlaySpawnSound();
        }
    }
}
