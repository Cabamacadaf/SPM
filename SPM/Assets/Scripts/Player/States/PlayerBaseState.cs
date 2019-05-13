using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    //Attributes
    protected Vector3 momentum;
    protected Vector3 direction;
    private float distance;
    private float size;
    private RaycastHit hitInfo;

    private int checkCollisionCounter = 0;
    protected Vector3 keyboardDirection;


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

        keyboardDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Quaternion cameraRotation = owner.mainCamera.transform.rotation;
        direction = cameraRotation * keyboardDirection;

        direction = owner.Movement.MoveAlongGround(direction);

        if (Input.GetKey(KeyCode.LeftControl)){
            owner.Transition<PlayerCrouchState>();
        }

        CameraRotation();

        if (Input.GetKeyDown(KeyCode.K)) {
            owner.Respawn();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (owner.hasFlashlight) {
                if (owner.flashlight.enabled) {
                    owner.flashlight.enabled = false;
                }
                else {
                    owner.flashlight.enabled = true;
                }
            }
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
