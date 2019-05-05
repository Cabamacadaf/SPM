using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class NotHoldingState : State
{
    private GravityGun owner;
    private GameObject lastPickUpObjectHit;
    private GravityBlast gravityBlast;

    public override void Initialize(StateMachine owner)
    {
        this.owner = (GravityGun)owner;
        gravityBlast = owner.GetComponent<GravityBlast>();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void HandleUpdate()
    {
        Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.hitLayer);

        if (hit.collider != null && hit.transform.GetComponent<PickUpObject>() != null)
        {
            if (lastPickUpObjectHit != null && hit.transform.gameObject != lastPickUpObjectHit)
            {
                lastPickUpObjectHit.GetComponent<PickUpObject>().UnHighlight();
            }
            lastPickUpObjectHit = hit.transform.gameObject;
            hit.transform.GetComponent<PickUpObject>().Highlight();
            owner.crosshair.color = Color.green;
        }

        else
        {
            if (lastPickUpObjectHit != null)
            {
                lastPickUpObjectHit.GetComponent<PickUpObject>().UnHighlight();
            }
            owner.crosshair.color = Color.red;
        }

        if (hit.collider != null && hit.transform.GetComponent<Enemy>() != null)
        {
            owner.crosshair.color = Color.yellow;
        }


        if (Input.GetMouseButtonDown(0))
        {
            Push();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Pull();
        }
    }

    public void Push()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.hitLayer) && hit.collider.attachedRigidbody != null)
        {
            if (hit.collider.GetComponent<PickUpObject>() != null)
            {
                // hit.collider.attachedRigidbody.isKinematic = false;
                hit.collider.attachedRigidbody.AddForce(Camera.main.transform.forward * owner.pushForce * (1 - (hit.distance / owner.pushRange)));
            }
            else if (hit.collider.gameObject.CompareTag("Platform"))
            {

                Platform platform = hit.collider.gameObject.GetComponent<Platform>();
                if (platform.IsActive)
                {
                    hit.collider.gameObject.GetComponent<Platform>().IsActive = false;

                }

            }
        }
    }

    public void Pull()
    {

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pullRange, owner.hitLayer))

            if (hit.transform.GetComponent<PickUpObject>() != null)
            {
                //hit.collider.attachedRigidbody.isKinematic = true;
                owner.holdingObject = hit.collider.GetComponent<PickUpObject>();
                owner.holdingObject.Pull(owner.pullForce);
                owner.Transition<HoldingState>();
            }
            else if (hit.collider.gameObject.CompareTag("Platform"))
            {

                Platform platform = hit.collider.gameObject.GetComponent<Platform>();
                if (!platform.IsActive)
                {
                    hit.collider.gameObject.GetComponent<Platform>().IsActive = true;

                }

            }

    }
}
