//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunBaseState : State
{
    private float objectXRotation;

    protected GravityGun owner;

    public override void Initialize (StateMachine owner)
    {
        this.owner = (GravityGun)owner;
        base.Initialize(owner);
    }

    public override void HandleUpdate ()
    {
        if (Input.GetMouseButtonDown(1) && (owner.GetCurrentState() is GravityGunNotHoldingState) == false) {
            DropObject();
        }

        PullPointRotation();

        base.HandleUpdate();
    }

    public override void HandleFixedUpdate ()
    {
        if (owner.holdingObject != null && (owner.GetCurrentState() is GravityGunHoldingState) == false || owner.holdingObject.IsColliding == false) {
            if (Vector3.Distance(owner.pullPoint.transform.position, owner.holdingObject.transform.position) > owner.distanceToGrab) {
                owner.holdingObject.transform.position += (owner.pullPoint.position - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime;
            }
            else {
                owner.holdingObject.transform.position = owner.pullPoint.transform.position;
            }
            //if (owner.holdingObject.transform.rotation != owner.pullPoint.rotation) {
            //    owner.holdingObject.transform.rotation = Quaternion.Lerp(owner.holdingObject.transform.rotation, owner.pullPoint.rotation, owner.objectRotationSpeed * Time.deltaTime);
            //}
        }

        base.HandleFixedUpdate();
    }

    private void PullPointRotation ()
    {
        objectXRotation = -Camera.main.transform.eulerAngles.x;
        owner.pullPoint.transform.localRotation = Quaternion.Euler(objectXRotation, 0, 0);
    }

    public void DropObject ()
    {
        owner.holdingObject.Drop();
        owner.holdingObject = null;
        owner.Transition<GravityGunNotHoldingState>();
    }
}
