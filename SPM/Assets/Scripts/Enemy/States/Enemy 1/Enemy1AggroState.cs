using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner.attackDistance && timer > owner.attackCooldown) {
            owner.agent.SetDestination(owner.transform.position);
            owner.agent.isStopped = true;
            owner.Transition<Enemy1AttackState>();
        }
    }
}
