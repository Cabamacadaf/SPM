using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/CrouchState")]


public class PlayerCrouchState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Collider.center /= 2;
        owner.Collider.height /= 2;
        owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = CameraManager.FIRSTPERSON;

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        //Physics.Raycast(owner.transform.position, Vector3.up, 6) || 

        if (owner.Movement.GetVelocity().magnitude < owner.CrouchSpeed)
        {
            owner.Movement.AddVelocity(direction * owner.Acceleration * Time.deltaTime);

        }

    }

    public override void Exit()
    {
        owner.Collider.center *= 2;
        owner.Collider.height *= 2;
        owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = CameraManager.THIRDPERSON;
        Debug.Log("ExitCrouch");
    }
}
