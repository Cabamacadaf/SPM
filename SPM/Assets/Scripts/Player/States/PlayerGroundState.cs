//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/GroundState")]


public class PlayerGroundState : PlayerBaseState
{
    private float timeToJumpAfterLeavingGround = 0.5f;
    private float timer;
    private bool timerRunning;
    public override void Enter()
    {
        base.Enter();
        timerRunning = false;
        Debug.Log("Enter Ground State");
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
       

        if (Input.GetKeyDown(KeyCode.Space) && IsCrouching == false)
        {
            Jump();
        }

        else if (IsGrounded() == false)
        {
            if(timerRunning == false)
            {
                timer = timeToJumpAfterLeavingGround;
                timerRunning = true;

            }
            if (timer <= 0)
            {
                Owner.Transition<PlayerAirState>();
            }
            timer -= Time.deltaTime;
        }
    }

    private void Jump ()
    {
        Owner.Velocity = new Vector3(Owner.Velocity.x, 0, Owner.Velocity.z);
        Owner.Velocity += Vector3.up * Owner.JumpHeight;
        Owner.Transition<PlayerAirState>();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
