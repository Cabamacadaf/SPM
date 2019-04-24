using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public override void Enter ()
    {
        base.Enter();
        owner.audioSource.PlayOneShot(owner.aggroSound);
        owner.lightSource.enabled = true;
        owner.agent.speed = owner.movementSpeed;
        owner.agent.acceleration = owner.acceleration;
        owner.agent.isStopped = false;
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
