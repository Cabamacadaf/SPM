using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public override void Enter ()
    {
        base.Enter();
        owner.lightSource.enabled = true;
        owner.agent.enabled = true;
        owner.agent.speed = owner.movementSpeed;
        owner.agent.acceleration = owner.acceleration;
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
