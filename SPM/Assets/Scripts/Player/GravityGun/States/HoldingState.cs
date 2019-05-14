//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class HoldingState : State
{
    private GravityGun owner;
    
    public override void Initialize(StateMachine owner)
    {
        this.owner = (GravityGun)owner;
    }

    public override void HandleUpdate()
    {
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
