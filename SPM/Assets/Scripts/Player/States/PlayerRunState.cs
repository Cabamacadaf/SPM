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

        if(owner.Movement.GetVelocity().magnitude < owner.Movement.RunningSpeed)
        {
            owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * Time.deltaTime);

        }

        owner.Movement.Running();


        if(owner.Movement.stamina <= 0 || Input.GetKeyUp(KeyCode.LeftShift))
        {
            owner.Transition<PlayerWalkState>();
        }
    }
}
