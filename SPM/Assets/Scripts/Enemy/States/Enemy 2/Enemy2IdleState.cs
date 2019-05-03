using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy/Enemy2/IdleState")]
public class Enemy2IdleState : EnemyIdleState
{
    public override void Enter ()
    {
        //Debug.Log("Idle State");
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
    }
}
