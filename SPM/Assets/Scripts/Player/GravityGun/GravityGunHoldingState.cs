using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityGunHoldingState : State
{
    private GravityGun owner;
    // Start is called before the first frame update


    public override void Initialize(StateMachine owner)
    {
        this.owner = (GravityGun)owner;
    }


    public void Push()
    {

        owner.holdingObject.Drop();
        owner.holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * pushForce);
        owner.holdingObject = null;
        
    }

    public void Pull()
    {  
         owner.holdingObject.Drop();
         owner.holdingObject = null;  
    }


}
