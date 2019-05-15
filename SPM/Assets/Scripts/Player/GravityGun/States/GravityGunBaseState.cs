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
            Debug.Log(owner.GetCurrentState());
            Debug.Log(owner.GetCurrentState() is GravityGunNotHoldingState);
            Debug.Log("Drop Object");
            DropObject();
        }

        base.HandleUpdate();
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

        PullPointRotation();
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
