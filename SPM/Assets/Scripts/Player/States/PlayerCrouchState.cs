using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/CrouchState")]


public class PlayerCrouchState : PlayerGroundState
{
    public override void Enter()
    {
        base.Enter();
        owner.Collider.center = new Vector3(5.221367e-05f, 5, 0.5494844f);
        owner.Collider.height = 10;
        owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = new Vector3(0, 5, 0);
        Debug.Log("EnterCrouch");

    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        Debug.Log("CrouchState");
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            owner.Transition<PlayerWalkState>();
        }
        //Physics.Raycast(owner.transform.position, Vector3.up, 6) || 

        if (owner.Movement.GetVelocity().magnitude < owner.Movement.CrouchSpeed)
        {
            owner.Movement.AddVelocity(direction * owner.Movement.Acceleration * Time.deltaTime);

        }

    }

    public override void Exit()
    {
        owner.Collider.center = new Vector3(5.221367e-05f, 9.45f, 0.5494844f);
        owner.Collider.height = 18.58f;
        owner.mainCamera.gameObject.GetComponent<CameraManager>().cameraOffset = new Vector3(7, 0, -12);
        Debug.Log("ExitCrouch");
    }
}
