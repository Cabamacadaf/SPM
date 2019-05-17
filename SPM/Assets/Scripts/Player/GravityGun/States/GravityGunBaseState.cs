//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunBaseState : State
{
    private float objectXRotation;

    private Vector3 moveToPosition;
    private Vector3 pullPointDirection;
    private RaycastHit pullPointDirectionCastHit;

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

        if (owner.holdingObject != null) {
            CheckPullPointRaycast();
            owner.holdingObject.LastFramePosition = owner.holdingObject.transform.position;
        }

        PullPointRotation();

        base.HandleUpdate();
    }

    public override void HandleFixedUpdate ()
    {
        if (owner.holdingObject != null) {
            if (Vector3.Distance(owner.pullPoint.position, owner.holdingObject.transform.position) > owner.distanceToGrab) {
                if (pullPointDirectionCastHit.collider != null) {
                    if (owner.holdingObject.transform.parent == owner.holdingObject.CurrentParent) {
                        owner.holdingObject.transform.SetParent(owner.holdingObject.OriginalParent);
                    }

                    if(Vector3.Distance(owner.pullPoint.position, owner.holdingObject.transform.position) > owner.distanceToDrop){
                        DropObject();
                    }

                    moveToPosition = (pullPointDirectionCastHit.point - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime; ;
                }

                else {
                    if (owner.holdingObject.transform.parent == owner.holdingObject.OriginalParent) {
                        owner.holdingObject.transform.SetParent(owner.holdingObject.CurrentParent);
                    }

                    moveToPosition = (owner.pullPoint.position - owner.holdingObject.transform.position).normalized * owner.pullForce * Time.deltaTime;
                }
                owner.holdingObject.transform.position += moveToPosition;
            }
            else {
                owner.holdingObject.transform.position = owner.pullPoint.position;
            }

            if ((owner.GetCurrentState() is GravityGunRotatingState) == false) {
                if (owner.isRotated == false) {
                    owner.holdingObject.transform.localRotation = Quaternion.Lerp(owner.holdingObject.transform.localRotation, owner.pullPoint.localRotation, owner.objectRotationSpeed * Time.deltaTime);
                }
                else {
                    owner.holdingObject.transform.localRotation = Quaternion.Lerp(owner.holdingObject.transform.localRotation, owner.objectRotation, owner.objectRotationSpeed * Time.deltaTime);
                }
            }
        }
        base.HandleFixedUpdate();
    }

    private void CheckPullPointRaycast ()
    {
        pullPointDirection = owner.pullPoint.position - owner.holdingObject.transform.position;
        Physics.Raycast(owner.holdingObject.transform.position, pullPointDirection.normalized, out pullPointDirectionCastHit, pullPointDirection.magnitude, owner.raycastCollideLayer);
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
