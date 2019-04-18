using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    private Enemy1 owner1;
    public override void Enter ()
    {
        Debug.Log("Aggro State");
        owner1 = (Enemy1)owner;
        base.Enter();
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();

        owner.agent.SetDestination(owner.player.transform.position);

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner1.attackDistance) {
            owner.agent.SetDestination(owner.transform.position);
            owner.agent.isStopped = true;
            owner.Transition<Enemy1AttackState>();
        }
    }
}
