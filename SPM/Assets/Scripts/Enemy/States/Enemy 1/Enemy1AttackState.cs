using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AttackState")]
public class Enemy1AttackState : EnemyAttackState
{
    private Enemy1 owner1;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner1 = (Enemy1)owner;
    }

    public override void Enter ()
    {
        EnemyAttackEvent enemyAttackEvent = new EnemyAttackEvent(owner.attackSound, owner.audioSource);
        enemyAttackEvent.ExecuteEvent();

        //Debug.Log("Attack State");
        base.Enter();
        owner.attackHitbox.SetActive(true);
    }

    public override void HandleUpdate ()
    {
        if (timer >= owner1.attackTime) {
            attackObject.position = owner.transform.position;
            owner.Transition<Enemy1AttackRecoverState>();
        }

        attackObject.position += attackObject.forward * Time.deltaTime * owner1.attackAnimationSpeed;
        timer += Time.deltaTime;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
    }
}
