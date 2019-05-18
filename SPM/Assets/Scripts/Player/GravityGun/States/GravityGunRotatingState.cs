using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/RotatingState")]
public class GravityGunRotatingState : GravityGunBaseState
{
    private CameraController cameraController;

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    public override void Initialize (StateMachine owner)
    {
        cameraController = owner.GetComponentInParent<CameraController>();
        base.Initialize(owner);
    }

    public override void Enter ()
    {
        rotationX = Owner.HoldingObject.transform.localEulerAngles.x;
        rotationY = Owner.HoldingObject.transform.localEulerAngles.y;
        Owner.IsRotated = true;
        cameraController.MouseControlOn = false;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            Owner.Transition<GravityGunHoldingState>();
        }
        rotationX -= Input.GetAxisRaw("Mouse Y") * Owner.RotationMouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * Owner.RotationMouseSensitivity;
        Owner.ObjectRotation = Quaternion.Euler(rotationX, rotationY, 0);
        Owner.HoldingObject.transform.localRotation = Owner.ObjectRotation;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        cameraController.MouseControlOn = true;
        base.Exit();
    }
}
