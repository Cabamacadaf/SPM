using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Enemy1/IdleState")]
public class Enemy1IdleState : EnemyIdleState
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