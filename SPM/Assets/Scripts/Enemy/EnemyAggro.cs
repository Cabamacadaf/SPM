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
        if (other.CompareTag("Player")) {
            if (enemy is Enemy2 && !(enemy.GetCurrentState() is Enemy2AggroState)) {
                enemy.Transition<Enemy2AggroState>();
            }
            else if (enemy is Enemy1 && !(enemy.GetCurrentState() is Enemy1AggroState)) {
                enemy.Transition<Enemy1AggroState>();
            }
        }
    }
}
