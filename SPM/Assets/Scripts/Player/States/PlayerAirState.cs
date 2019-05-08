using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    private float speed;
    private float modifier = 0.01f;
    public override void Enter()
    {
        modifier = 0.01f;
        base.Enter();
        speed = owner.Movement.GetVelocity().magnitude;
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        modifier += 0.001f;
        //owner.Movement.AddVelocity(Vector2.down * modifier);
        owner.Movement.AddVelocity(direction * speed * Time.deltaTime);
        //if (owner.Movement.GetVelocity().magnitude > speed)
        //{


        //    owner.Movement.SetVelocity(owner.Movement.GetVelocity().normalized * speed);


        //}
        if (owner.Movement.IsGrounded())
        {
            
            owner.Transition<PlayerIdleState>();
        }


    }

    public override void Exit()
    {


    }
}
