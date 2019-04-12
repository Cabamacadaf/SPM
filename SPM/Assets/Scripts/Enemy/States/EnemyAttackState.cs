using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private float timer = 0.0f;
    private Transform attackObject;

    public override void Enter ()
    {
        base.Enter();
        timer = 0.0f;
        attackObject = owner.transform.GetChild(0);
        Attack();
    }

    private void Attack ()
    {

    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if(timer >= owner.attackTime) {
            attackObject.position = owner.transform.position;
            owner.Transition<EnemyAggroState>();
        }

        attackObject.position += attackObject.forward * Time.deltaTime * owner.attackAnimationSpeed;
        timer += Time.deltaTime;
    }
}
