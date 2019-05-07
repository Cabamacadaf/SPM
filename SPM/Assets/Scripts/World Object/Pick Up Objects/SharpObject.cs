using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpObject : PickUpObject
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy2Hurtbox")) {
            Enemy2 enemy = other.GetComponentInParent<Enemy2>();
            EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();

            enemyState.Damage(impactDamage * enemy.damageReduction);
        }
    }
}
