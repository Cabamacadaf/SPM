using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/ManipulatingState")]
public class GravityGunManipulateState : GravityGunBaseState
{
    private bool isHovering;
    public override void Enter()
    {
        Owner.ActivePlatform.IsActive = true;
    }

    public override void HandleUpdate()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit aimRaycastHit, Owner.PullRange, Owner.RaycastCollideLayer);
        if(aimRaycastHit.collider != null)
        {
            isHovering = aimRaycastHit.collider.gameObject.Equals(Owner.ActivePlatform.gameObject);

        }

        if (Input.GetMouseButtonDown(1) || isHovering == false)
        {
            Owner.ActivePlatform.IsActive = false;
            Owner.ActivePlatform = null;

            Owner.Transition<GravityGunNotHoldingState>();


        }
    }

    public override void Exit()
    {
    }

}
