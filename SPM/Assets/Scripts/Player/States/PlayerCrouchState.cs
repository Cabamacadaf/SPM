using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/CrouchState")]


public class PlayerCrouchState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Collider.center = new Vector3(0, 0.40f, 0);
        owner.Collider.height = 0.80f;
        //owner.mainCamera.gameObject.GetComponent<CameraManager>().ParentCamera.transform.localPosition = CameraManager.FIRSTPERSON;
        //owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = Vector3.zero;
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
        owner.Collider.center = new Vector3(0, 0.93f, 0);
        owner.Collider.height = 1.86f;
        //owner.mainCamera.gameObject.GetComponent<CameraManager>().ParentCamera.transform.localPosition = new Vector3(0, 1.5f, 0);
        //owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = CameraManager.THIRDPERSON;
        Debug.Log("ExitCrouch");
    }
}
