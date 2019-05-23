//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/GroundState")]


public class PlayerGroundState : PlayerBaseState
{
    private float speed;
    private bool canStand;
    private bool isCrouching;

    public override void Enter()
    {
        Debug.Log("Enter Ground State");
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

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
        SetVelocity();
    }

    private void Jump()
    {
        Velocity.y += Owner.JumpHeight;
        Owner.Transition<PlayerAirState>();
    }

    private void SetVelocity()
    {
        //Debug.Log("Stamina: " + Owner.Stamina.Stamina);
        if (Input.GetKey(KeyCode.LeftShift) && Owner.Stamina.Stamina > 0)
        {
            speed = Owner.SprintSpeed;
            Owner.Stamina.UseStamina();
        }
        else
        {
            Owner.Stamina.RecoverStamina();
            canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;

            if (Input.GetKey(KeyCode.LeftControl))
            {
                isCrouching = true;
                Owner.CrouchSetup();
                //canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;
                speed = Owner.CrouchSpeed;
            }
            else if(Input.GetKey(KeyCode.LeftControl) == false && isCrouching && canStand == false){
                speed = Owner.CrouchSpeed;

            }
            else if (Input.GetKey(KeyCode.LeftControl) == false && isCrouching  && canStand)
            {

                Owner.NormalSetup();
                speed = Owner.WalkSpeed;
                isCrouching = false;
            }
            else
            {
                speed = Owner.WalkSpeed;
                isCrouching = false;
            }


        }
        //Velocity += Direction * Owner.Acceleration * Time.deltaTime;
        Velocity.x = Direction.x * speed;
        Velocity.z = Direction.z * speed;
    }

    public override void Exit()
    {
        Debug.Log("Exit Ground State");
    }
}
