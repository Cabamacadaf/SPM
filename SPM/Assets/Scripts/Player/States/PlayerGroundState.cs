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

            owner.physics.AddVelocity(Vector2.up * owner.jumpHeight);
            owner.Transition<PlayerAirState>();

        }
        base.HandleUpdate();

        owner.physics.AddVelocity(base.direction * owner.groundAcceleration * Time.deltaTime);

    }
}
