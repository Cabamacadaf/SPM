//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class GravityGunNotHoldingState : GravityGunBaseState
{
    private Highlight lastPickUpObjectHitHighlight;
    private RaycastHit aimRaycastHit;

    private Platform currentPlatform;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        base.HandleUpdate();
        
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out aimRaycastHit, Owner.PullRange, Owner.RaycastCollideLayer);

        if (aimRaycastHit.collider != null && Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, Owner.PullLayer)) {
            if (lastPickUpObjectHitHighlight != null && aimRaycastHit.transform.gameObject != lastPickUpObjectHitHighlight) {
                lastPickUpObjectHitHighlight.Deactivate();
            }
            lastPickUpObjectHitHighlight = aimRaycastHit.transform.GetComponent<Highlight>();
            lastPickUpObjectHitHighlight.Activate();
            Owner.Crosshair.color = Color.green;
        }

        else if (aimRaycastHit.collider != null && aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
            Owner.Crosshair.color = Color.green;
        }

        else {
            if (lastPickUpObjectHitHighlight != null) {
                lastPickUpObjectHitHighlight.Deactivate();
            }
            Owner.Crosshair.color = Color.red;
        }

        if (Input.GetMouseButtonDown(1)) {
            Pull();
        }

        //if(currentPlatform != null)
        //{
            
        //    if (aimRaycastHit.collider.gameObject.CompareTag("Platform") == false && currentPlatform.IsActive)
        //    {
        //        Debug.Log("Hello");
        //        currentPlatform.IsActive = false;
        //    }
        //}

    }

    public void Pull ()
    {
        if (aimRaycastHit.collider != null) {
            if (Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, Owner.PullLayer)) {
                Owner.HoldingObject = aimRaycastHit.collider.GetComponent<PickUpObject>();
                Owner.Transition<GravityGunPullingState>();
            }
            else if (aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
                Platform platform = aimRaycastHit.collider.gameObject.GetComponent<Platform>();
                currentPlatform = platform;
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
