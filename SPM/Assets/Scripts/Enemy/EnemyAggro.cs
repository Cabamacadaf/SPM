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
        if (other.CompareTag("Player") && !enemy.GetCurrentState().ToString().Equals("EnemyAggroState1")) {
            enemy.Transition<Enemy1AggroState>();
        }
    }
}
