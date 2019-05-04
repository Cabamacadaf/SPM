using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/AttackState")]
public class EnemyAttackState : EnemyBaseState
{
    protected float timer = 0.0f;

    public override void Enter ()
    {
        timer = 0.0f;
        EnemyAttackEvent enemyAttackEvent = new EnemyAttackEvent(owner.attackSound, owner.audioSource);
        enemyAttackEvent.ExecuteEvent();

        //Debug.Log("Attack State");
        owner.attackObject.SetActive(true);
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (timer >= owner.attackTime) {
            owner.Transition<EnemyAttackRecoverState>();
        }

        owner.attackObject.transform.position += owner.attackObject.transform.forward * Time.deltaTime * owner.attackAnimationSpeed;
        timer += Time.deltaTime;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        base.Exit();
        owner.attackObject.transform.position = owner.transform.position;
        owner.attackObject.SetActive(false);
        owner.GetComponentInChildren<Attack>().hasAttacked = false;
    }
}
