//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/HoldingState")]
public class GravityGunHoldingState : GravityGunBaseState
{
    private bool isCharging;
    private float timer;

    private Vector3 wallPassThroughRaycastDirection;

    public override void Enter ()
    {
        isCharging = false;
        timer = 0;
        Owner.HoldingObject.Hold(Owner.PullPoint.position, Owner.transform);
        Owner.Crosshair.color = Color.yellow;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        CheckPassThroughWallRaycast();

        if (UpgradeSettings.instance.HasUpgrade) {
            UpgradedGravityGun();
        }

        else if (Input.GetMouseButtonDown(0)) {
            Owner.HoldingObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * Owner.PushForce);
            DropObject(true);
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            Owner.Transition<GravityGunRotatingState>();
        }

        base.HandleUpdate();
    }

    public override void HandleFixedUpdate ()
    {
        base.HandleFixedUpdate ();
    }

    private void UpgradedGravityGun ()
    {
        if (Input.GetMouseButton(0) && timer <= UpgradeSettings.instance.MaxTime) {
            Owner.HoldingObject.ImpactDamage += UpgradeSettings.instance.GrowRate;
            Owner.PushForce += UpgradeSettings.instance.GrowRate;
            timer += Time.deltaTime;
            isCharging = true;

        }

        else if (isCharging) {
            Addforce();
            DropObject(true);
        }
    }

    private void CheckPassThroughWallRaycast ()
    {
        wallPassThroughRaycastDirection = Owner.HoldingObject.transform.position - Owner.HoldingObject.LastFramePosition;
        Physics.Raycast(Owner.HoldingObject.LastFramePosition, wallPassThroughRaycastDirection.normalized, out RaycastHit hit, wallPassThroughRaycastDirection.magnitude, Owner.RaycastCollideLayer);
        if (hit.collider != null) {
            Owner.HoldingObject.transform.position = Owner.HoldingObject.LastFramePosition;
            DropObject(false);
        }
    }

    private void Addforce ()
    {
        Vector3 direction = Camera.main.transform.forward;
        float force = Owner.PushForce;
        Owner.HoldingObject.GetComponent<Rigidbody>().AddForce(direction * force);
    }
}
