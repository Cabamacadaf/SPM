//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/PullingState")]
public class GravityGunPullingState : GravityGunBaseState
{
    public override void Enter ()
    {
        Owner.HoldingObject.Pull();
        base.Enter();
    }

    public override void HandleFixedUpdate ()
    {
        if(Vector3.Distance(Owner.HoldingObject.transform.position, Owner.PullPoint.position) < Owner.DistanceToGrab) {
            Owner.Transition<GravityGunHoldingState>();
        }
        base.HandleFixedUpdate();
    }
}
