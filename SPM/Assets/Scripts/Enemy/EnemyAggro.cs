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
            Debug.DrawLine(other.bounds.center, enemy.transform.position, Color.yellow, 2);
            Vector3 raycastDirection = enemy.transform.position - other.bounds.center;
            if (!Physics.Raycast(other.bounds.center, raycastDirection.normalized, out RaycastHit hit, raycastDirection.magnitude, wallLayer)) {
                if ((enemy is Enemy2 && enemy.GetCurrentState() is Enemy2IdleState) || (enemy is Enemy1 && enemy.GetCurrentState() is Enemy1IdleState)) {
                    Aggro();
                }
                gameObject.SetActive(false);
            }
        }
    }

    public void Aggro ()
    {
        EnemyAggroEvent enemyAggroEvent = new EnemyAggroEvent(enemy.aggroSound, enemy.audioSource);
        enemyAggroEvent.ExecuteEvent();

        if (enemy is Enemy1) {
            enemy.Transition<Enemy1AggroState>();
        }

        if (enemy is Enemy2) {
            enemy.Transition<Enemy2AggroState>();
        }
    }
}
