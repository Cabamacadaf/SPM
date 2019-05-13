using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "States/Player/CrouchState")]


public class PlayerCrouchState : PlayerBaseState
{
    public override void Enter()
    {
        base.Enter();
        owner.Collider.center = new Vector3(0, owner.CrouchColliderCenter, 0);
        owner.Collider.height = owner.CrouchColliderHeight;
        owner.GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, owner.CrouchCameraHeight, 0);
        owner.gravityGun.transform.localPosition = new Vector3(0.44f, owner.CrouchGravityGunHeight, 0.57f);


    }

    public override void HandleUpdate()
    {
        base.HandleUpdate();

        //Physics.Raycast(owner.transform.position, Vector3.up, 6) || 

        owner.Movement.AddVelocity(direction * owner.Acceleration * Time.deltaTime);


        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            owner.Transition<PlayerIdleState>();
        }
    }

    public override void Exit()
    {
        owner.Collider.center = new Vector3(0, 0.93f, 0);
        owner.Collider.height = 1.86f;
        owner.GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, 1.7f, 0);
        owner.gravityGun.transform.localPosition = new Vector3(0.44f, 1.34f, 0.57f);
        Debug.Log("ExitCrouch");
    }
}
