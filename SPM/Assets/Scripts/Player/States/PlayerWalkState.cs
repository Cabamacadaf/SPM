using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/WalkState")]

public class PlayerWalkState : PlayerGroundState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            owner.Transition<PlayerCrouchState>();
        }
    
        owner.Stamina.Recover();

        if (Input.GetKey(KeyCode.LeftShift) && owner.Stamina.Stamina >= owner.Stamina.MaxStamina)
        {
            owner.Transition<PlayerRunState>();
        }

        if (owner.Movement.GetVelocity().magnitude <= owner.GetWalkSpeed())
        {

            owner.Movement.AddVelocity(direction * owner.Acceleration * Time.deltaTime);


        }


    }
}
