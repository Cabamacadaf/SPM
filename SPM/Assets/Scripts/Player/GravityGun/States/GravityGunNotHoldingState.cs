//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class GravityGunNotHoldingState : GravityGunBaseState
{
    private PickUpObject lastPickUpObjectHit;

    public override void Enter ()
    {
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.raycastCollideLayer);
        
        if (hit.collider != null && Layers.IsInLayerMask(hit.collider.gameObject.layer, owner.pullLayer)) {
            if (lastPickUpObjectHit != null && hit.transform.gameObject != lastPickUpObjectHit) {
                lastPickUpObjectHit.UnHighlight();
            }
            lastPickUpObjectHit = hit.transform.GetComponent<PickUpObject>();
            lastPickUpObjectHit.Highlight();
            owner.crosshair.color = Color.green;
        }

        else if (hit.collider != null && hit.collider.gameObject.CompareTag("Platform"))
        {
            owner.crosshair.color = Color.green;
        }

        else {
            if (lastPickUpObjectHit != null) {
                lastPickUpObjectHit.UnHighlight();
            }
            owner.crosshair.color = Color.red;
        }

        if (Input.GetMouseButtonDown(0)) {
            Push();
        }

        if (Input.GetMouseButtonDown(1)) {
            Pull();
        }
        base.HandleUpdate();
    }

    public void Push ()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.raycastCollideLayer)
            && hit.collider.attachedRigidbody != null) {
            if (Layers.IsInLayerMask(hit.collider.gameObject.layer, owner.pullLayer)) {
                hit.collider.attachedRigidbody.AddForce(Camera.main.transform.forward * owner.pushForce * (1 - (hit.distance / owner.pushRange)));
            }
            else if (hit.collider.gameObject.CompareTag("Platform")) {
                Platform platform = hit.collider.gameObject.GetComponent<Platform>();
                if (platform.IsActive) {
                    hit.collider.gameObject.GetComponent<Platform>().IsActive = false;
                }
            }
        }
    }

    public void Pull ()
    {

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.forward, out RaycastHit hit, owner.pullRange, owner.raycastCollideLayer)) {
            if (Layers.IsInLayerMask(hit.collider.gameObject.layer, owner.pullLayer)) {
                owner.holdingObject = hit.collider.GetComponent<PickUpObject>();
                owner.Transition<GravityGunPullingState>();
            }
            else if (hit.collider.gameObject.CompareTag("Platform")) {
                Platform platform = hit.collider.gameObject.GetComponent<Platform>();
                if (!platform.IsActive) {
                    hit.collider.gameObject.GetComponent<Platform>().IsActive = true;
                }
                else {
                    hit.collider.gameObject.GetComponent<Platform>().IsActive = false;
                }
            }
        }
    }
}
