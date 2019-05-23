//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    private float timer;

    public override void Enter()
    {
        base.Enter();
        timer = 15f;
        Debug.Log("Enter Air State");
    }

    public override void HandleUpdate()
    {
        if (IsGrounded())
        {
            Owner.Transition<PlayerGroundState>();
        }

        base.HandleUpdate();
        Direction =  Vector3.ProjectOnPlane(Direction, Vector3.up).normalized;
        
        //if (IsGrounded() && timer <= 0)
        //{
        //    Debug.Log("Trans");
        //    Owner.Transition<PlayerGroundState>();
        //}
        //timer--;
    }

    public override void Exit()
    {
        Debug.Log("Exit Air State");
    }
}
