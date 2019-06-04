//Author: Marcus Mellström

using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;
    private Enemy enemy;

    private void Awake ()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.CompareTag("Player")) {
            Vector3 raycastDirection = enemy.Collider.bounds.center - other.bounds.center;
            if (Physics.Raycast(other.bounds.center, raycastDirection.normalized, raycastDirection.magnitude, wallLayer) == false) {
                if ((enemy.GetCurrentState() is EnemyIdleState)) {
                    EnemyIdleState enemyIdleState = (EnemyIdleState)enemy.GetCurrentState();
                    enemyIdleState.Aggro();
                }
                gameObject.SetActive(false);
            }
        }
    }

    public void Aggro ()
    {
        enemy.AudioSource.PlayOneShot(enemy.AttackSounds[Random.Range(0, enemy.AttackSounds.Length)]);
        if (enemy is Enemy1) {
            enemy.Transition<Enemy1AggroState>();
        }

        if (enemy is Enemy2) {
            enemy.Transition<Enemy2AggroState>();
        }
    }
}
