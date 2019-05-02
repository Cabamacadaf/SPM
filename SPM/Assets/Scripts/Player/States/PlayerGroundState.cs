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
        RaycastHit hitInfo;
        if (Physics.Raycast(owner.transform.position, Vector3.down, out hitInfo))
        {
            direction = Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }
        owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * Time.deltaTime);

    }
}
