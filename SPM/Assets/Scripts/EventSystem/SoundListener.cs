//Author: Marcus Mellström

using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private void Start ()
    {
        EnemyAttackEvent.RegisterListener(OnEnemyAttack);
        EnemyAggroEvent.RegisterListener(OnEnemyAggro);
    }

    private void OnEnemyAttack(EnemyAttackEvent enemyAttackEvent)
    {
        enemyAttackEvent.audioSource.PlayOneShot(enemyAttackEvent.audioClip);
    }

    private void OnEnemyAggro(EnemyAggroEvent enemyAggroEvent)
    {
        enemyAggroEvent.audioSource.PlayOneShot(enemyAggroEvent.audioClip);
    }
}
