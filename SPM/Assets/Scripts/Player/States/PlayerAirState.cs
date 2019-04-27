using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/AirState")]

public class PlayerAirState : PlayerBaseState
{
    // Methods
    public override void Enter()
    {
        base.Enter();

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        RaycastHit hitInfo;
        if (Physics.SphereCast(owner.transform.position + base.point2, owner.capsuleCollider.radius, Vector3.down, out hitInfo, owner.groundCheckDistance + owner.skinWidth, owner.walkableMask))
        {
            owner.Transition<PlayerGroundState>();
        }

    }
}
