using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTrigger : MonoBehaviour
{
    [SerializeField] private Enemy[] enemies;
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player")) {

            foreach (Enemy enemy in enemies) {
                if ((enemy.GetCurrentState() is EnemyIdleState)) {
                    EnemyIdleState enemyIdleState = (EnemyIdleState)enemy.GetCurrentState();
                    enemyIdleState.Aggro();
                }
            }
            gameObject.SetActive(false);
        }
    }
}
