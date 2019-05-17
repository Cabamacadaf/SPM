//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class GravityGunNotHoldingState : GravityGunBaseState
{
    private Highlight lastPickUpObjectHitHighlight;
    private RaycastHit aimRaycastHit;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimRaycastHit, owner.pullRange, owner.raycastCollideLayer);

        if (aimRaycastHit.collider != null && Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, owner.pullLayer)) {
            if (lastPickUpObjectHitHighlight != null && aimRaycastHit.transform.gameObject != lastPickUpObjectHitHighlight) {
                lastPickUpObjectHitHighlight.Deactivate();
            }
            lastPickUpObjectHitHighlight = aimRaycastHit.transform.GetComponent<Highlight>();
            lastPickUpObjectHitHighlight.Activate();
            Debug.Log("Activate");
            owner.crosshair.color = Color.green;
        }

        else if (aimRaycastHit.collider != null && aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
            owner.crosshair.color = Color.green;
        }

        else {
            if (lastPickUpObjectHitHighlight != null) {
                lastPickUpObjectHitHighlight.Deactivate();
            }
            owner.crosshair.color = Color.red;
        }

        if (Input.GetMouseButtonDown(1)) {
            Pull();
        }
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
