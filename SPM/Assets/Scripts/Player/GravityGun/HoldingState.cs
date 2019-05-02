using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class HoldingState : State
{
    private GravityGun owner;
    // Start is called before the first frame update


    public override void Initialize(StateMachine owner)
    {
        this.owner = (GravityGun)owner;
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

        if (Input.GetMouseButtonDown(0))
        {
            Push();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Drop();
        }
    }

    public void Push()
    {

        owner.holdingObject.Drop();
        owner.holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * owner.pushForce);
        owner.holdingObject = null;
        owner.Transition<NotHoldingState>();
    }

    public void Drop()
    {
        owner.holdingObject.Drop();
        owner.holdingObject = null;
        owner.Transition<NotHoldingState>();
    }
}
