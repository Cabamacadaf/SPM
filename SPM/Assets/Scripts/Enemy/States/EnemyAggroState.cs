using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAggroState : EnemyBaseState
{
    public override void Enter ()
    {
        //Debug.Log("Aggro State");
        owner.lightSource.enabled = true;
        owner.agent.enabled = true;
        owner.agent.speed = owner.movementSpeed;
        owner.agent.acceleration = owner.acceleration;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
