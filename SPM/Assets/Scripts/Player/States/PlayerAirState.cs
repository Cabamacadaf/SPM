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
        base.Enter();
        //timer = 0.1f;
        Debug.Log("Enter Air State");
    }

    public override void HandleUpdate()
    {
        //if (IsGrounded() && timer <= 0) {
        //    Owner.Transition<PlayerGroundState>();
        //    timer = 0.1f;
        //}
        //timer -= Time.deltaTime;

        base.HandleUpdate();

        if (IsGrounded()) {
            Owner.Transition<PlayerGroundState>();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
