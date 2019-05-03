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


    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (owner.Movement.IsGrounded())
        {
            owner.Transition<PlayerWalkState>();
        }
        owner.Movement.AddVelocity(base.direction * speed * Time.deltaTime);


    }
}
