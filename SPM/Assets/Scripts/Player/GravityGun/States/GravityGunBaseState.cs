//Author: Marcus Mellström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GravityGunBaseState : State
{
    private float objectXRotation;

    private Vector3 moveToPosition;
    private Vector3 pullPointDirection;
    private RaycastHit pullPointDirectionCastHit;

    protected GravityGun Owner { get; set; }

    public override void Initialize (StateMachine owner)
    {
        Owner = (GravityGun)owner;
        base.Initialize(owner);
    }

    public override void HandleUpdate ()
    {
        if (Input.GetMouseButtonDown(1) && (Owner.GetCurrentState() is GravityGunNotHoldingState) == false) {
            DropObject(false);
        }

        if (Owner.HoldingObject != null) {
            CheckPullPointRaycast();
            Owner.HoldingObject.LastFramePosition = Owner.HoldingObject.transform.position;
        }

        PullPointRotation();

        base.HandleUpdate();
    }

    public override void HandleFixedUpdate ()
    {
        if (Owner.HoldingObject != null) {
            if (Vector3.Distance(Owner.PullPoint.position, Owner.HoldingObject.transform.position) > Owner.DistanceToGrab) {
                //If object is trying to move through a wall
                if (pullPointDirectionCastHit.collider != null) {
                    if (Owner.HoldingObject.transform.parent == Owner.HoldingObject.CurrentParent) {
                        Owner.HoldingObject.transform.SetParent(Owner.HoldingObject.OriginalParent);
                    }

                    if(Vector3.Distance(Owner.PullPoint.position, Owner.HoldingObject.transform.position) > Owner.DistanceToDrop){
                        DropObject(false);
                    }

                    moveToPosition = (pullPointDirectionCastHit.point - Owner.HoldingObject.transform.position).normalized * Owner.PullForce * Time.deltaTime; ;
                }

                else {
                    if (Owner.HoldingObject.transform.parent == Owner.HoldingObject.OriginalParent) {
                        Owner.HoldingObject.transform.SetParent(Owner.HoldingObject.CurrentParent);
                    }

                    moveToPosition = (Owner.PullPoint.position - Owner.HoldingObject.transform.position).normalized * Owner.PullForce * Time.deltaTime;
                }
                Owner.HoldingObject.transform.position += moveToPosition;
            }
            else {
                Owner.HoldingObject.transform.position = Owner.PullPoint.position;
            }

            if ((Owner.GetCurrentState() is GravityGunRotatingState) == false) {
                if (Owner.IsRotated == false) {
                    Owner.HoldingObject.transform.localRotation = Quaternion.Lerp(Owner.HoldingObject.transform.localRotation, Owner.PullPoint.localRotation, Owner.ObjectRotationSpeed * Time.deltaTime);
                }
                else {
                    Owner.HoldingObject.transform.localRotation = Quaternion.Lerp(Owner.HoldingObject.transform.localRotation, Owner.ObjectRotation, Owner.ObjectRotationSpeed * Time.deltaTime);
                }
            }
        }
        base.HandleFixedUpdate();
    }

    private void CheckPullPointRaycast ()
    {
        pullPointDirection = Owner.PullPoint.position - Owner.HoldingObject.transform.position;
        Physics.Raycast(Owner.HoldingObject.transform.position, pullPointDirection.normalized, out pullPointDirectionCastHit, pullPointDirection.magnitude, Owner.RaycastCollideLayer);
    }

    private void PullPointRotation ()
    {
        objectXRotation = -Camera.main.transform.eulerAngles.x;
        Owner.PullPoint.localRotation = Quaternion.Euler(objectXRotation, 0, 0);
    }

    public void DropObject (bool isThrown)
    {
        Owner.HoldingObject.Drop(isThrown);
        Owner.HoldingObject = null;
        Owner.Transition<GravityGunNotHoldingState>();
    }
}
