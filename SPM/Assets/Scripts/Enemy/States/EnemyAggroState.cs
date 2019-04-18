using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    protected float timer;

    public override void Initialize (StateMachine owner)
    {
        base.Initialize(owner);
        timer = this.owner.attackCooldown;
    }

    public override void Enter ()
    {
        base.Enter();
        timer = 0.0f;
        owner.agent.speed = owner.movementSpeed;
        owner.agent.acceleration = owner.acceleration;
        owner.agent.isStopped = false;
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        timer += Time.deltaTime;
        if (Vector3.Distance(owner.player.transform.position, owner.transform.position) > owner.attackDistance) {
            //owner.transform.position = Vector3.MoveTowards(owner.transform.position, owner.player.transform.position, owner.movementSpeed * Time.deltaTime);
            //owner.transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(owner.transform.forward, owner.player.transform.position - owner.transform.position, owner.rotationSpeed * Time.deltaTime, 0.0f));

            owner.agent.SetDestination(owner.player.transform.position);
        }
    }
}
