using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/GravityGun/RotatingState")]
public class GravityGunRotatingState : GravityGunBaseState
{
    [SerializeField] private float mouseSensitivity = 1.0f;
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
        rotationX = owner.holdingObject.transform.localEulerAngles.x;
        rotationY = owner.holdingObject.transform.localEulerAngles.y;
        owner.isRotated = true;
        cameraController.MouseControlOn = false;
        base.Enter();
    }

    public override void HandleUpdate ()
    {
        if(Input.GetKeyDown(KeyCode.R)) {
            owner.Transition<GravityGunHoldingState>();
        }
        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        owner.objectRotation = Quaternion.Euler(rotationX, rotationY, 0);
        owner.holdingObject.transform.localRotation = owner.objectRotation;
        base.HandleUpdate();
    }

    public override void Exit ()
    {
        cameraController.MouseControlOn = true;
        base.Exit();
    }
}
