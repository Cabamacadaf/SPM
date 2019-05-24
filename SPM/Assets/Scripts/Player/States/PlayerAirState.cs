//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Air State");
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        Direction = Vector3.ProjectOnPlane(Direction, Vector3.up).normalized;


        if (IsGrounded()) {
            Owner.Transition<PlayerGroundState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
