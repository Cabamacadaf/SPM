using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private void Start ()
    {
        EnemyAttackEvent.RegisterListener(OnEnemyAttack);
        EnemyAggroEvent.RegisterListener(OnEnemyAggro);
    }

    void OnEnemyAttack(EnemyAttackEvent enemyAttackEvent)
    {
        enemyAttackEvent.audioSource.PlayOneShot(enemyAttackEvent.audioClip);
    }

    void OnEnemyAggro(EnemyAggroEvent enemyAggroEvent)
    {
        enemyAggroEvent.audioSource.PlayOneShot(enemyAggroEvent.audioClip);
    }
}
