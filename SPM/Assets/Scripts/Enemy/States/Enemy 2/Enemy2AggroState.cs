using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy2/AggroState")]
public class Enemy2AggroState : EnemyAggroState
{
    private Enemy2 owner2;
    private float timer;
    public override void Enter()
    {
        timer = 0.0f;
        Debug.Log("Aggro State");
        owner2 = (Enemy2)owner;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.agent.SetDestination(owner.player.transform.position);
        timer += Time.deltaTime;

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner2.leapRange && timer > owner2.leapCooldown) {
            owner.agent.SetDestination(owner.transform.position);
            owner.agent.isStopped = true;
            owner.Transition<EnemyLeapChargeState>();
        }
    }
    public override void Exit ()
    {
        base.Exit();
    }
}
