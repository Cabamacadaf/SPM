//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    //private float timer;

    public override void Enter()
    {
        //Velocity.y += Owner.JumpHeight;
        base.Enter();
        //timer = 0.1f;
        Debug.Log("Enter Air State");
    }

    public override void HandleUpdate()
    {
        if (IsGrounded()) {
            Owner.Transition<PlayerGroundState>();
        }
        //if (IsGrounded() && timer <= 0) {
        //    Debug.Log("Trans");
        //    Owner.Transition<PlayerGroundState>();
        //    timer = 0.1f;
        //}
        //timer -= Time.deltaTime;

        base.HandleUpdate();
    }

    public override void Exit()
    {
        Debug.Log("Exit Air State");
    }
}
