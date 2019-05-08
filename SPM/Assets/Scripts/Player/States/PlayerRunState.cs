using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/RunState")]

public class PlayerRunState : PlayerBaseState
{

    

    public override void Enter()
    {
        base.Enter();
        owner.Movement.airResistanceCoefficient = 0.1f;

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();
        owner.Movement.AddVelocity(direction * owner.RunningSpeed * Time.deltaTime);

        //if (owner.Movement.GetVelocity().magnitude <= owner.GetWalkSpeed() * 2f)
        //{



        //}

        owner.Stamina.Running();

        if (owner.Stamina.Stamina <= 0 || Input.GetKeyUp(KeyCode.LeftShift))
        {
            owner.Transition<PlayerIdleState>();
        }
        else if (keyboardDirection.magnitude == 0)
        {
            owner.Transition<PlayerIdleState>();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {


            owner.Movement.AddVelocity(Vector2.up * owner.JumpHeight);
            owner.Transition<PlayerAirState>();

        }

     
    }

    public override void Exit()
    {
        base.Exit();

        owner.Movement.airResistanceCoefficient = 0.1f;
    }
}
