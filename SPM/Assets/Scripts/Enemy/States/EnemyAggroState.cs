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
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        timer += Time.deltaTime;
    }
}
