using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    protected float timer = 0.0f;
    protected Transform attackObject;

    public override void Enter ()
    {
        base.Enter();
        timer = 0.0f;
        attackObject = owner.transform.GetChild(0);
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
