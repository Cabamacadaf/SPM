using System.Collections;
using System.Collections.Generic;
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
                
                if (enemy is Enemy2 && enemy.GetCurrentState() is Enemy2IdleState) {
                    enemy.audioSource.PlayOneShot(enemy.aggroSound);
                    enemy.Transition<Enemy2AggroState>();
                }
                else if (enemy is Enemy1 && enemy.GetCurrentState() is Enemy1IdleState) {
                    enemy.audioSource.PlayOneShot(enemy.aggroSound);
                    enemy.Transition<Enemy1AggroState>();
                }
                gameObject.SetActive(false);
            }
        }
    }
}
