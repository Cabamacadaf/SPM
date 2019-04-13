using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AttackState")]
public class Enemy1AttackState : EnemyAttackState
{
    public override void Enter ()
    {
        base.Enter();
        owner.attacking = true;
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (timer >= owner.attackTime) {
            attackObject.position = owner.transform.position;
            owner.Transition<Enemy1AggroState>();
        }

        attackObject.position += attackObject.forward * Time.deltaTime * owner.attackAnimationSpeed;
        timer += Time.deltaTime;
    }

    public override void Exit ()
    {
        base.Exit();
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
        owner.attacking = false;
    }
}
