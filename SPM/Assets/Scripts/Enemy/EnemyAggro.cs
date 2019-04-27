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
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {
            Vector3 raycastDirection = enemy.transform.position - other.transform.position;
            if (!Physics.Raycast(other.transform.position, raycastDirection.normalized, out RaycastHit hit, raycastDirection.magnitude, wallLayer)) {
                if (enemy is Enemy2 && enemy.GetCurrentState() is Enemy2IdleState) {
                    enemy.Transition<Enemy2AggroState>();
                }
                else if (enemy is Enemy1 && enemy.GetCurrentState() is Enemy1IdleState) {
                    enemy.Transition<Enemy1AggroState>();
                }
            }
        }
    }
}
