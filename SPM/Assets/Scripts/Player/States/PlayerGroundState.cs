using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/Player/GroundState")]

public class PlayerGroundState : PlayerBaseState
{
    // Attributes

    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {

            owner.Movement.AddVelocity(Vector2.up * owner.Movement.JumpHeight);
            owner.Transition<PlayerAirState>();

        }
        base.HandleUpdate();

        owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * Time.deltaTime);

    }
}
