using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/RunState")]

public class PlayerRunState : PlayerGroundState
{



    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if(owner.Movement.GetVelocity().magnitude <= owner.GetWalkSpeed()*1.5f)
        {

            owner.Movement.AddVelocity(direction * owner.Acceleration * Time.deltaTime);

        }

        owner.Stamina.Running();


        if(owner.Stamina.Stamina <= 0 || Input.GetKeyUp(KeyCode.LeftShift))
        {
            owner.Transition<PlayerWalkState>();
        }
    }
}
