using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    private float speed;
    // Methods
    public override void Enter()
    {
        base.Enter();
        speed = owner.Movement.GetVelocity().magnitude;
        owner.Movement.gravity = 150;


    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (owner.Movement.GetVelocity().magnitude <= owner.Movement.WalkingSpeed)
        {
            owner.Movement.AddVelocity(direction * speed * Time.deltaTime);


        }


        if (owner.Movement.IsGrounded())
        {
            owner.Transition<PlayerWalkState>();
        }



    }

    public override void Exit()
    {
        owner.Movement.gravity = 90;

    }
}
