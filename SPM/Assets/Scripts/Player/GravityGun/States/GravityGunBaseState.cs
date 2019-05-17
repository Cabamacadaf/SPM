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
                    Debug.DrawRay(owner.holdingObject.transform.position, pullPointDirection, new Color(255, 100, 0));
                    Debug.DrawLine(owner.holdingObject.transform.position, pullPointDirectionCastHit.point, Color.green);

                    if (owner.holdingObject.transform.parent == owner.holdingObject.CurrentParent) {
                        owner.holdingObject.transform.SetParent(owner.holdingObject.OriginalParent);
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
            owner.holdingObject.transform.localRotation = Quaternion.Lerp(owner.holdingObject.transform.localRotation, owner.pullPoint.localRotation, owner.objectRotationSpeed * Time.deltaTime);
        }
        base.HandleFixedUpdate();
    }

    private void CheckPullPointRaycast ()
    {
        Debug.Log(owner.holdingObject.transform);
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
