using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/Player/IdleState")]

public class PlayerIdleState : PlayerBaseState
{
    protected float turnSpeedModifier = 5f;

    
    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
       

            owner.Movement.AddVelocity(Vector2.up * owner.JumpHeight);
            owner.Transition<PlayerAirState>();

        }
        else if (Input.GetKey(KeyCode.LeftShift) && owner.Stamina.Stamina >= owner.Stamina.MaxStamina)
        {
            owner.Transition<PlayerRunState>();
        }
        else if(keyboardDirection.magnitude != 0)
        {
            owner.Transition<PlayerWalkState>();
        }
        
        //RaycastHit hitInfo;
        //if (Physics.Raycast(owner.transform.position, Vector3.down, out hitInfo))
        //{
        //    direction = Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        //}


    }
}
