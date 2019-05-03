using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/WalkState")]

public class PlayerWalkState : PlayerGroundState
{

    public override void Enter()
    {
        //Debug.Log("Enter Walk");
        base.Enter();
    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        if (Input.GetKey(KeyCode.LeftControl))
        {
            owner.Transition<PlayerCrouchState>();
        }
    
        owner.Movement.Recover();

        if (Input.GetKey(KeyCode.LeftShift) && owner.Movement.stamina >= PlayerMovement.FULL_STAMINA)
        {
            owner.Transition<PlayerRunState>();
        }


        if (owner.Movement.GetVelocity().magnitude < owner.Movement.WalkingSpeed)
        {
            float dotProduct = Vector3.Dot(direction, owner.Movement.GetVelocity().normalized);
            //Debug.Log("DotProduct: " + dotProduct);

            if (dotProduct <= 0 && Input.GetKey(KeyCode.W))
            {
                //Debug.Log("Turnspeed");

                owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * turnSpeedModifier * Time.deltaTime);
            }
            else
            {
                owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * Time.deltaTime);

            }

        }
     

    }
}
