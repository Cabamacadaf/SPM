//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunBaseState : State
{
    protected GravityGun owner;

    public override void Initialize (StateMachine owner)
    {
        this.owner = (GravityGun)owner;
        base.Initialize(owner);
    }

    public override void HandleUpdate ()
    {
        owner.holdingObject.transform.rotation = Quaternion.Lerp(owner.holdingObject.transform.rotation, owner.pullPoint.rotation, owner.objectRotationSpeed * Time.deltaTime);
        owner.pullPoint.transform.rotation = Quaternion.identity;
        base.HandleUpdate();
    }
}
