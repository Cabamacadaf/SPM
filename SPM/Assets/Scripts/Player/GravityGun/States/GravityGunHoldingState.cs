//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class GravityGunHoldingState : GravityGunBaseState
{
    public override void Enter ()
    {
        owner.holdingObject.Holding(owner.pullPoint.position, owner.transform);
        owner.crosshair.color = Color.yellow;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            owner.holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * owner.pushForce);
            Drop();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Drop();
        }
        base.HandleUpdate();
    }

    public void Drop()
    {
        owner.Transition<GravityGunNotHoldingState>();
    }

    public override void Exit ()
    {
        owner.holdingObject.Drop();
        owner.holdingObject = null;
        base.Exit();
    }
}
