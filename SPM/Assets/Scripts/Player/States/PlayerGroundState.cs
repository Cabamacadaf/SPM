//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/GroundState")]


public class PlayerGroundState : PlayerBaseState
{
    private float timeToJumpAfterLeavingGround = 0.1f;
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
        Direction = Vector3.ProjectOnPlane(Direction, GroundHitInfo.normal).normalized;

        Debug.Log(Owner.Velocity.magnitude);
        if(Owner.Velocity.magnitude < 1.0f) {
            Owner.Velocity = Vector3.zero;
        }

        //Vector3 GroundCross = Vector3.Cross(GroundHitInfo.normal, Direction);
        //Debug.Log(GroundCross);

        //GroundAngle = Vector3.Angle(GroundHitInfo.normal, Owner.transform.forward);
        //Debug.Log(GroundHitInfo.normal.y != 1);
        //Debug.Log("Forward: " + (Owner.transform.forward));
        //if(GroundHitInfo.normal.y != 1 && Owner.transform.forward == Vector3.zero && Owner.transform.right == Vector3.zero)
        //{
        //    Debug.Log("Hello");
        //    Owner.Velocity = Vector3.zero;
        //}
        //Debug.Log("Ground Angle: " + (180 - GroundAngle));
        
   
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
