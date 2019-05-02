using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/AggroState")]
public class Enemy2AggroState : EnemyAggroState
{
    private Enemy2 owner2;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        owner2 = (Enemy2)owner;
    }

    public override void Enter ()
    {
        Debug.Log("Aggro State");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        owner.agent.SetDestination(owner.player.transform.position);

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner2.leapRange) {
            owner.agent.SetDestination(owner.transform.position);
            owner.agent.isStopped = true;
            owner.Transition<Enemy2LeapChargeState>();
        }
        base.HandleUpdate();
    }
    public override void Exit ()
    {
        base.Exit();
    }
}
