﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class NotHoldingState : State
{
    private GravityGun owner;
    private GameObject lastPickUpObjectHit;

    public override void Initialize(StateMachine owner)
    {
  
        this.owner = (GravityGun)owner;
       
    }

    public override void Enter()
    {
     

        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
        {
            if(lastPickUpObjectHit != null && hit.transform.gameObject != lastPickUpObjectHit) {
                lastPickUpObjectHit.GetComponent<PickUpObject>().UnHighlight();
            }
            lastPickUpObjectHit = hit.transform.gameObject;
            hit.transform.GetComponent<PickUpObject>().Highlight();
            owner.crosshair.color = Color.green;
        }

        else
        {
            if (lastPickUpObjectHit != null) {
                lastPickUpObjectHit.GetComponent<PickUpObject>().UnHighlight();
            }
            owner.crosshair.color = Color.red;
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

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.hitLayer) 
            && hit.transform.GetComponent<PickUpObject>() != null)
        {
            if (hit.collider.attachedRigidbody != null && hit.collider.GetComponent<PickUpObject>() != null)
            {
                hit.collider.attachedRigidbody.isKinematic = false;
                hit.collider.attachedRigidbody.AddForce(Camera.main.transform.forward * owner.pushForce * (1 - (hit.distance / owner.pushRange)));
            }
        }



    }

    public void Pull()
    {

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pullRange, owner.hitLayer)
            && hit.transform.GetComponent<PickUpObject>() != null)
        {
            hit.collider.attachedRigidbody.isKinematic = true;
            owner.holdingObject = hit.collider.GetComponent<PickUpObject>();
            owner.holdingObject.Pull(owner.pullForce);
            owner.Transition<HoldingState>();
        }


    }





}