using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/WalkState")]

public class PlayerWalkState : PlayerBaseState
{

    public override void Enter()
    {
      
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (keyboardDirection.magnitude == 0)
        {
            owner.Transition<PlayerIdleState>();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {


            owner.Movement.AddVelocity(Vector2.up * owner.JumpHeight);
            owner.Transition<PlayerAirState>();

        }
        else if (Input.GetKey(KeyCode.LeftShift) && owner.Stamina.Stamina >= owner.Stamina.MaxStamina)
        {
            owner.Transition<PlayerRunState>();
        }

        owner.Stamina.Recover();


        //Vector3 groundVel = new Vector3(owner.Movement.GetVelocity().x, 0, owner.Movement.GetVelocity().z);
        owner.Movement.AddVelocity(direction * owner.Acceleration * Time.deltaTime);
        //if (owner.Movement.GetVelocity().magnitude > owner.GetWalkSpeed())
        //{


        //    owner.Movement.SetVelocity(owner.Movement.GetVelocity().normalized * owner.GetWalkSpeed());


        //}




    }
}
