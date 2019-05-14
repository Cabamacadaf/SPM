//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/PullingState")]
public class GravityGunPullingState : GravityGunBaseState
{
    public override void Enter ()
    {
        owner.holdingObject.Pull();
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        owner.holdingObject.transform.position += (owner.pullPoint.position - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime;

        if(Vector3.Distance(owner.holdingObject.transform.position, owner.pullPoint.position) < owner.distanceToGrab) {
            owner.Transition<GravityGunHoldingState>();
        }

        base.HandleUpdate();
    }
}
