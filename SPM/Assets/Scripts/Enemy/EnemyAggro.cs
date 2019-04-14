using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggro : MonoBehaviour
{
    private Enemy enemy;

    private void Awake ()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player") && (!enemy.GetCurrentState().ToString().Equals("Enemy1AggroState") || !enemy.GetCurrentState().ToString().Equals("Enemy2AggroState"))) {
            if (enemy is Enemy2) {
                enemy.Transition<Enemy2AggroState>();
            }
            else if (enemy is Enemy) {
                enemy.Transition<Enemy1AggroState>();
            }
        }
    }
}
