//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class GravityGunNotHoldingState : GravityGunBaseState
{
    private PickUpObject lastPickUpObjectHit;
    private RaycastHit aimRaycastHit;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        if (Input.GetMouseButtonDown(1)) {
            Pull();
        }
    }

    public override void HandleFixedUpdate ()
    {
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimRaycastHit, owner.pullRange, owner.raycastCollideLayer);

        if (aimRaycastHit.collider != null && Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, owner.pullLayer)) {
            if (lastPickUpObjectHit != null && aimRaycastHit.transform.gameObject != lastPickUpObjectHit) {
                lastPickUpObjectHit.UnHighlight();
            }
            lastPickUpObjectHit = aimRaycastHit.transform.GetComponent<PickUpObject>();
            lastPickUpObjectHit.Highlight();
            owner.crosshair.color = Color.green;
        }

        else if (aimRaycastHit.collider != null && aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
            owner.crosshair.color = Color.green;
        }

        else {
            if (lastPickUpObjectHit != null) {
                lastPickUpObjectHit.UnHighlight();
            }
            owner.crosshair.color = Color.red;
        }
        base.HandleFixedUpdate();
    }

    public void Pull ()
    {
        if (aimRaycastHit.collider != null) {
            if (Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, owner.pullLayer)) {
                owner.holdingObject = aimRaycastHit.collider.GetComponent<PickUpObject>();
                owner.Transition<GravityGunPullingState>();
            }
            else if (aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
                Platform platform = aimRaycastHit.collider.gameObject.GetComponent<Platform>();
                if (!platform.IsActive) {
                    aimRaycastHit.collider.gameObject.GetComponent<Platform>().IsActive = true;
                }
                else {
                    aimRaycastHit.collider.gameObject.GetComponent<Platform>().IsActive = false;
                }
            }
        }
    }
}
