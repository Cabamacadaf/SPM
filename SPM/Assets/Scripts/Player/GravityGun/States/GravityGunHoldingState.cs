//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class GravityGunHoldingState : GravityGunBaseState
{


    private bool isCharging;
    private float timer;

    public override void Enter()
    {
        isCharging = false;
        timer = 0;
        owner.holdingObject.Hold(owner.pullPoint.position, owner.transform);
        owner.crosshair.color = Color.yellow;
        base.Enter();
    }

    public override void HandleUpdate()
    {
        if (UpgradeSettings.instance.HasUpgrade)
        {
       
            UpgradedGravityGun();
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                owner.holdingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * owner.pushForce);
                DropObject();
            }
        }


        base.HandleUpdate();
    }

    private void UpgradedGravityGun()
    {

        
       
        if (Input.GetMouseButton(0) &&  timer <= UpgradeSettings.instance.MaxTime)
        {
            owner.holdingObject.ImpactDamage += UpgradeSettings.instance.GrowRate;
            owner.pushForce += UpgradeSettings.instance.GrowRate;
            timer += Time.deltaTime;
            isCharging = true;

        }
        else if (isCharging)
        {
            Addforce();
            DropObject();

        }


    }

    private void Addforce()
    {
        Vector3 direction = Camera.main.transform.forward;
        float force = owner.pushForce;
        owner.holdingObject.GetComponent<Rigidbody>().AddForce(direction * force);

    }
}
