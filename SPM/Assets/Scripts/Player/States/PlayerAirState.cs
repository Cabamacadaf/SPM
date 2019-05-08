using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    private float modifier = 0.01f;
    public override void Enter()
    {
        modifier = 0.01f;
        base.Enter();

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        modifier += 0.001f;
        owner.Movement.AddVelocity(Vector2.down * modifier);


        if (owner.Movement.IsGrounded())
        {
            
            owner.Transition<PlayerIdleState>();
        }


    }

    public override void Exit()
    {


    }
}
