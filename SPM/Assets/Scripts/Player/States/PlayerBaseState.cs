using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    //Attributes

    protected Vector3 direction;
    private float distance;
    private float size;
    private RaycastHit hitInfo;

    private int checkCollisionCounter = 0;


    protected Player owner;




    public override void Enter ()
    {


    }

    public override void Initialize (StateMachine owner)
    {
        this.owner = (Player)owner;
    }



    public override void HandleUpdate ()
    {
        HandleInput();
    
    }

    private void HandleInput ()
    {

        Vector3 keyboardDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Quaternion cameraRotation = owner.mainCamera.transform.rotation;
        direction = cameraRotation * keyboardDirection;

        RaycastHit hitInfo;
        if (Physics.SphereCast(owner.transform.position + owner.Movement.point2, owner.Movement.capsuleCollider.radius, Vector3.down, out hitInfo, owner.Movement.GroundCheckDistance + owner.Movement.SkinWidth, owner.Movement.walkableMask))
        {
            direction = Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }


        CameraRotation();

        if (Input.GetKeyDown(KeyCode.K)) {
            owner.Respawn();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (owner.flashlight.enabled) {
                owner.flashlight.enabled = false;
            }
            else {
                owner.flashlight.enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(0)) {
            owner.gravityGun.Push();
        }

        if (Input.GetMouseButtonDown(1)) {
            owner.gravityGun.Pull();
        }

    }

    private void CameraRotation ()
    {
        var cameraRotation = owner.mainCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        owner.transform.rotation = cameraRotation;
        cameraRotation = owner.mainCamera.transform.rotation;
        owner.gravityGun.transform.rotation = cameraRotation;
    }


}
