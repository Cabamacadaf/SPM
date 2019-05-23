//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/GroundState")]


public class PlayerGroundState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Ground State");
    }

    public override void HandleUpdate()
    {
        if (Physics.Raycast(Owner.transform.position, Vector3.down, out RaycastHit hitInfo))
        {
            Direction = Vector3.ProjectOnPlane(Direction, hitInfo.normal).normalized;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump");
            Jump();
        }

        //else if(IsGrounded())
        //{
        //    Owner.Transition<PlayerAirState>();
        //}
        base.HandleUpdate();
    }

    private void Jump ()
    {
        Debug.Log("Jumping");
        Owner.Velocity += Vector3.up * Owner.JumpHeight;
        Owner.Transition<PlayerAirState>();
    }

    public override void Exit()
    {
        Debug.Log("Exit Ground State");
    }
}
