//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class GravityGunNotHoldingState : GravityGunBaseState
{
    private Highlight lastPickUpObjectHitHighlight;
    private RaycastHit aimRaycastHit;

    private Vector3 pushDirection;

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

        if (Input.GetMouseButtonDown(0)) {
            Push();
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

    private void Push ()
    {
        if (aimRaycastHit.collider != null) {
            if (Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, Owner.PullLayer)) {
                pushDirection = Camera.main.transform.forward;
                pushDirection = new Vector3(pushDirection.x, 0.0f, pushDirection.z).normalized;
                aimRaycastHit.collider.attachedRigidbody.AddForce(pushDirection * Owner.PushForce);
            }
        }
    }

    private void Pull ()
    {
        if (aimRaycastHit.collider != null) {
            if (Functions.IsInLayerMask(aimRaycastHit.collider.gameObject.layer, Owner.PullLayer)) {
                Owner.HoldingObject = aimRaycastHit.collider.GetComponent<PickUpObject>();
                Owner.Transition<GravityGunPullingState>();
            }
            else if (aimRaycastHit.collider.gameObject.CompareTag("Platform")) {
                Owner.ActivePlatform = aimRaycastHit.collider.gameObject.GetComponent<Manipulate>();
                Owner.ActivePlatform.IsActive = true;
            }
        }
    }
}
