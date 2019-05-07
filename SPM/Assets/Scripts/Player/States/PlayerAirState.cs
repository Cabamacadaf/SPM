using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
 
    public override void Enter()
    {
        base.Enter();

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();


        if (owner.Movement.IsGrounded())
        {
            owner.Transition<PlayerWalkState>();
        }



    }

    public override void Exit()
    {


    }
}
