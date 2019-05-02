﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/NotHoldingState")]
public class NotHoldingState : State
{
    private GravityGun owner;

    public override void Initialize(StateMachine owner)
    {
        Debug.Log("Init");
        this.owner = (GravityGun)owner;
       
    }

    public override void Enter()
    {
        Debug.Log("Enter");

        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pushRange, owner.hitLayer) && hit.transform.GetComponent<PickUpObject>() != null)
        {
            owner.crosshair.color = Color.green;
        }

        else
        {
            owner.crosshair.color = Color.red;
        }
        Debug.Log("HandleUpdate");

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
                hit.collider.attachedRigidbody.AddForce(Camera.main.transform.forward * owner.pushForce * (1 - (hit.distance / owner.pushRange)));
            }
        }



    }

    public void Pull()
    {

        if (Physics.Raycast(Camera.main.transform.position + Camera.main.transform.forward * owner.cameraOffset, Camera.main.transform.forward, out RaycastHit hit, owner.pullRange, owner.hitLayer)
            && hit.transform.GetComponent<PickUpObject>() != null)
        {
            owner.holdingObject = hit.collider.GetComponent<PickUpObject>();
            owner.holdingObject.Pull(owner.pullForce);
            owner.Transition<HoldingState>();
        }


    }





}
