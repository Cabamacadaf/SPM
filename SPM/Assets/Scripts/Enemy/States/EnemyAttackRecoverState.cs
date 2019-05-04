using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackRecoverState")]
public class EnemyAttackRecoverState : EnemyBaseState
{
    private float timer;

    public override void Enter ()
    {
        //Debug.Log("Attack Recover State");
        base.Enter();
        timer = 0.0f;
    }

    public override void HandleUpdate ()
    {
        owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));

        timer += Time.deltaTime;

        if (timer >= owner.attackCooldown) {
            if (owner is Enemy1) {
                owner.Transition<Enemy1AggroState>();
            }
            else if (owner is Enemy2) {
                owner.Transition<Enemy2AggroState>();
            }
        }
        base.HandleUpdate();
    }
}
