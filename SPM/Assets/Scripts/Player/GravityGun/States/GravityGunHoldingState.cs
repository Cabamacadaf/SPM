//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class GravityGunHoldingState : GravityGunBaseState
{
    public override void Enter ()
    {
        owner.holdingObject.Hold(owner.pullPoint.position, owner.transform);
        owner.crosshair.color = Color.yellow;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if (Input.GetMouseButtonDown(0)) {
            owner.holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * owner.pushForce);
            DropObject();
        }

        if (Input.GetMouseButtonDown(1)) {
            DropObject();
        }

        base.HandleUpdate();
    }

    public override void HandleFixedUpdate ()
    {
        if (Vector3.Distance(owner.pullPoint.transform.position, owner.holdingObject.transform.position) > owner.distanceToDrop) {
            //DropObject();
        }
        base.HandleFixedUpdate();
    }
}
