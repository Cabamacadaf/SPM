using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AttackState")]
public class Enemy1AttackState : EnemyAttackState
{
    private Enemy1 owner1;
    public override void Enter ()
    {
        Debug.Log("Attack State");
        base.Enter();
        owner1 = (Enemy1)owner;
        owner.attackHitbox.SetActive(true);
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (timer >= owner1.attackTime) {
            attackObject.position = owner.transform.position;
            owner.Transition<Enemy1AttackRecoverState>();
        }

        attackObject.position += attackObject.forward * Time.deltaTime * owner1.attackAnimationSpeed;
        timer += Time.deltaTime;
    }

    public override void Exit ()
    {
        base.Exit();
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
    }
}
