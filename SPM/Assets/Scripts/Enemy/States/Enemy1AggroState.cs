using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/AggroState")]
public class Enemy1AggroState : EnemyAggroState
{
    public override void Enter ()
    {
        base.Enter();
        owner.agent.speed = owner.movementSpeed;
        owner.agent.isStopped = false;
    }
    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.attackDistance) {
            //owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.player.transform.position, owner.movementSpeed * Time.deltaTime);
            //owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));
            owner.agent.SetDestination(owner.player.transform.position);
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) < owner.attackDistance && timer > owner.attackCooldown) {
            owner.agent.isStopped = true;
            owner.Transition<Enemy1AttackState>();
        }

        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.chaseDistance) {
            owner.Transition<Enemy1IdleState>();
        }
    }
}
