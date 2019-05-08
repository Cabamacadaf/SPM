//Author: Marcus Mellström

using UnityEngine;

//Används inte just nu
public class EnemyStopTrigger : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Enemy enemy = other.GetComponent<Enemy>();
            if (other.CompareTag("Player") && (!enemy.GetCurrentState().ToString().Equals("Enemy1AggroState") || !enemy.GetCurrentState().ToString().Equals("Enemy2AggroState"))) {
                if (enemy is Enemy2) {
                    enemy.Transition<Enemy2IdleState>();
                }
                else if (enemy is Enemy) {
                    enemy.Transition<Enemy1IdleState>();
                }
            }
            enemy.agent.SetDestination(other.transform.position);
        }
    }
}
