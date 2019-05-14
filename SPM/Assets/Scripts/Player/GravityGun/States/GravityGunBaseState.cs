//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunBaseState : State
{
    private float xRotation;
    private float yRotation;
    private float zRotation;

    protected GravityGun owner;

    public override void Initialize (StateMachine owner)
    {
        this.owner = (GravityGun)owner;
        base.Initialize(owner);
    }

    public override void HandleFixedUpdate ()
    {
        if (owner.holdingObject != null) {
            if (Vector3.Distance(owner.pullPoint.transform.position, owner.holdingObject.transform.position) > owner.distanceToGrab) {
                owner.holdingObject.transform.position += (owner.pullPoint.position - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime;
            }
            else {
                owner.holdingObject.transform.position = owner.pullPoint.transform.position;
            }
            if (owner.holdingObject.transform.rotation != owner.pullPoint.rotation) {
                owner.holdingObject.transform.rotation = Quaternion.Lerp(owner.holdingObject.transform.rotation, owner.pullPoint.rotation, owner.objectRotationSpeed * Time.deltaTime);
            }
        }

        if (owner.holdingObject != null && owner.holdingObject.transform.rotation != owner.pullPoint.rotation) {
            owner.holdingObject.transform.rotation = Quaternion.Lerp(owner.holdingObject.transform.rotation, owner.pullPoint.rotation, owner.objectRotationSpeed * Time.deltaTime);
        }

        PullPointRotation();
        base.HandleFixedUpdate();
    }

    private void PullPointRotation ()
    {
        xRotation = 0;
        yRotation = owner.pullPoint.transform.eulerAngles.y;
        zRotation = owner.pullPoint.transform.eulerAngles.z;
        owner.pullPoint.transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
    }

    public void DropObject ()
    {
        owner.holdingObject.Drop();
        owner.holdingObject = null;
        owner.Transition<GravityGunNotHoldingState>();
    }
}
