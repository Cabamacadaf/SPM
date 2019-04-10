using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "States/Player/GroundState")]

public class PlayerGroundState : PlayerBaseState
{
    // Attributes
    [SerializeField] private float jumpHeight;

    // Methods
    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {

            owner.physics.AddVelocity(Vector2.up * jumpHeight);
            owner.Transition<PlayerAirState>();

        }
        base.HandleUpdate();
    }
}
