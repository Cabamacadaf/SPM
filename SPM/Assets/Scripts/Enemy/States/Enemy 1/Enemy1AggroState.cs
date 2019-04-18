using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    private float timer;
    public override void Enter ()
    {
        Debug.Log("Aggro State");
        timer = 0.0f;
        base.Enter();
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        
        owner.agent.SetDestination(owner.player.transform.position);
        timer += Time.deltaTime;
        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner.attackDistance ) {
            owner.agent.SetDestination(owner.transform.position);
            if (timer > owner.attackCooldown) {
                owner.agent.isStopped = true;
                owner.Transition<Enemy1AttackState>();
            }
        }
    }
}
