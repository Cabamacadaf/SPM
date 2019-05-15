//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunBaseState : State
{
    private float objectXRotation;
    private float collisionPullForceReduction = 0.25f;

    private Vector3 moveToPosition;
    private Vector3 moveDirection;

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
        if (owner.holdingObject != null) {
            if (Vector3.Distance(owner.pullPoint.position, owner.holdingObject.transform.position) > owner.distanceToGrab) {
                moveToPosition = (owner.pullPoint.position - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime;
                moveDirection = owner.pullPoint.position - owner.holdingObject.transform.position;

                if (Physics.Raycast(owner.holdingObject.transform.position, moveDirection.normalized, moveDirection.magnitude, owner.raycastCollideLayer)) {
                    if (owner.holdingObject.transform.parent == owner.holdingObject.CurrentParent) {
                        owner.holdingObject.transform.SetParent(owner.holdingObject.OriginalParent);
                    }
                    moveToPosition *= collisionPullForceReduction;
                }
                else if(owner.holdingObject.transform.parent == owner.holdingObject.OriginalParent) {
                    owner.holdingObject.transform.SetParent(owner.holdingObject.CurrentParent);
                }

                owner.holdingObject.transform.position += moveToPosition;
            }
            else {
                owner.holdingObject.transform.position = owner.pullPoint.position;
            }
            //Debug.Log("Object rotaion: " + owner.holdingObject.transform.localRotation);
            //Debug.Log("Pullpoint rotation: " + owner.pullPoint.localRotation);
            owner.holdingObject.transform.localRotation = Quaternion.Lerp(owner.holdingObject.transform.localRotation, owner.pullPoint.localRotation, owner.objectRotationSpeed * Time.deltaTime);
        }

        base.HandleFixedUpdate();
    }

    private void PullPointRotation ()
    {
        objectXRotation = -Camera.main.transform.eulerAngles.x;
        owner.pullPoint.localRotation = Quaternion.Euler(objectXRotation, 0, 0);
    }

    public void DropObject ()
    {
        owner.holdingObject.Drop();
        owner.holdingObject = null;
        owner.Transition<GravityGunNotHoldingState>();
    }
}
