//Author Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{


    //Attributes
    protected Player Owner;
    protected Vector3 Direction { get; set; }
    protected float SkinWidth { get; private set; }

    private float speed;
    private bool canStand;
    protected bool IsCrouching { get; set; }

    //Collision Check
    private Vector3 point1;
    protected Vector3 point2;
    private Vector3 snapSum;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;

    private bool canSprint = true;

    protected RaycastHit GroundHitInfo;
    protected float GroundAngle;

    public override void Initialize (StateMachine owner)
    {
        Owner = (Player)owner;
    }

    public override void Enter()
    {
    
        point1 = Owner.Collider.center + Vector3.up * ((Owner.Collider.height / 2) - Owner.Collider.radius);
        point2 = Owner.Collider.center + Vector3.down * ((Owner.Collider.height / 2) - Owner.Collider.radius);
        
    }

    public override void HandleUpdate ()
    {
        HandleInput();
        ApplyGravity();
        SetVelocity();
        CheckCollision(Owner.Velocity * Time.deltaTime);
        ResetValues();
    }

    #region Movement
    private void HandleInput()
    {
        HandleDirection();
        CameraRotation();
        HandleFlashLight();



    }


    private void HandleDirection()
    {

        Direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Quaternion cameraRotation = Owner.mainCamera.transform.rotation;
        Direction = Quaternion.Euler(0, cameraRotation.eulerAngles.y, cameraRotation.eulerAngles.z) * Direction;


        if (Owner.GetCurrentState() is PlayerGroundState)
        {

            Direction = Vector3.ProjectOnPlane(Direction, GroundHitInfo.normal).normalized;
        }
        else
        {
            Direction = Vector3.ProjectOnPlane(Direction, Vector3.up).normalized;
        }
        //Direction = new Vector3(Direction.x, 0, Direction.z);
    }

    protected void SetVelocity()
    {
        //Debug.Log("Stamina: " + Owner.Stamina.Stamina);
        if(canSprint == false && Owner.Stamina.Stamina > 30)
        {
            canSprint = true;
        }
        if (Input.GetKey(KeyCode.LeftShift) && Owner.Stamina.Stamina > 0 && IsCrouching == false && Owner.GetCurrentState() is PlayerGroundState && canSprint)
        {
            speed = Owner.SprintSpeed;
            Owner.Stamina.UseStamina();

            if (Owner.Stamina.Stamina <= 0)
            {
                Debug.Log("cannot sprint");
                canSprint = false;
            }
            //if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            //    Owner.Stamina.UseStamina();

            //}
            //else
            //{
            //    Owner.Stamina.RecoverStamina();

            //}
        }
        else
        {
            Owner.Stamina.RecoverStamina();
            
            canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;

            if (Input.GetKey(KeyCode.LeftControl) && Owner.GetCurrentState() is PlayerGroundState)
            {
                IsCrouching = true;
                Owner.CrouchSetup();
                //canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;
                speed = Owner.CrouchSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftControl) == false && IsCrouching && canStand == false)
            {
                speed = Owner.CrouchSpeed;

            }
            else if (Input.GetKey(KeyCode.LeftControl) == false && IsCrouching && canStand)
            {

                Owner.NormalSetup();
                speed = Owner.WalkSpeed;
                IsCrouching = false;
            }
            else
            {
                speed = Owner.WalkSpeed;
                IsCrouching = false;
            }


        }
 

        if(Owner.GetCurrentState() is PlayerGroundState) {
            if(Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            {
                Owner.Velocity = new Vector3(Direction.x * speed, 0, Direction.z * speed);

            }
            else
            {
                Owner.Velocity = new Vector3(Direction.x * speed, Owner.Velocity.y, Direction.z * speed);

            }

            if (Owner.Velocity.magnitude < 1.0f){
                Owner.Velocity = Vector3.zero;

            }
        }
        else
        {
            Owner.Velocity = new Vector3(Direction.x * speed, Owner.Velocity.y, Direction.z * speed);

        }
    }

    //Roterar spelaren
    private void CameraRotation()
    {
        var cameraRotation = Owner.mainCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        Owner.transform.rotation = cameraRotation;
        //Owner.GravityGunObject.transform.rotation = cameraRotation;
        cameraRotation = Owner.mainCamera.transform.rotation;
    }

    private void ResetValues()
    {
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }
    #endregion

    #region Physics
    public void ApplyGravity()
    {
        Owner.Velocity -=  Vector3.up * Owner.Gravity * Time.deltaTime;
    }

    private void ApplyAirResistance()
    {
        Owner.Velocity *= Mathf.Pow(Owner.AirResistanceCoefficient, Time.deltaTime);
    }

    private void ApplyFriction(float normalForceMagnitude)
    {
        Debug.Log("AppylFriction");
        if (Owner.Velocity.magnitude < CalculateStaticFriction(normalForceMagnitude))
        {
            Debug.Log("Zero");
            Owner.Velocity = Vector3.zero;
        }
        //else
        //{
        //    Owner.Velocity += -Owner.Velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);
        //}
    }

    private float CalculateStaticFriction(float normalForceMagnitude)
    {
        Debug.Log("StaticFrictionCoefficient: " + Owner.StaticFrictionCoefficient);
        return Functions.CalculateFriction(normalForceMagnitude, Owner.StaticFrictionCoefficient);
    }

    private float CalculateDynamicFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, Owner.DynamicFrictionCoefficient);
    }
    #endregion

    #region Collision/Groundcheck
    private void CheckCollision(Vector3 movement)
    {
        point1 = Owner.Collider.center + Vector3.up * ((Owner.Collider.height / 2) - Owner.Collider.radius);
        point2 = Owner.Collider.center + Vector3.down * ((Owner.Collider.height / 2) - Owner.Collider.radius);

        RaycastHit hitInfo;
        if (Physics.CapsuleCast(Owner.transform.position + point1, Owner.transform.position + point2, Owner.Collider.radius, Owner.Velocity.normalized, out hitInfo, Mathf.Infinity, Owner.WalkableMask)) {
            float impactAngle = Vector3.Angle(movement.normalized, hitInfo.normal) - 90;
            float hypotenuse = Owner.SkinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

            if(Mathf.Approximately(Mathf.Sin(impactAngle * Mathf.Deg2Rad), 0.0f)) {
                hypotenuse = Owner.SkinWidth;
            }
            Vector3 snapMovement = movement.normalized * (hitInfo.distance - hypotenuse);
            snapMovement = Vector3.ClampMagnitude(snapMovement, movement.magnitude);

            movement -= snapMovement;

            Vector3 hitNormalForceMovement = Functions.CalculateNormalForce(movement, hitInfo.normal);

            movement += hitNormalForceMovement;

            if(hitNormalForceMovement.sqrMagnitude > 0.00001f) {
                Vector3 hitNormalForceVelocity = Functions.CalculateNormalForce(Owner.Velocity, hitInfo.normal);
                Owner.Velocity += hitNormalForceVelocity;
            }
            //ApplyFriction(Owner.Velocity.magnitude);


            Owner.transform.position += snapMovement;

            if(movement.sqrMagnitude > 0.00001f && checkCollisionCounter < maxLoopValue) {
                checkCollisionCounter++;
                CheckCollision(movement);
            }

        }
        else if(movement.sqrMagnitude > 0.00001f)
        {
            //ApplyFriction(Owner.Velocity.magnitude);
            Owner.transform.position += movement;
        }

    }

    public bool IsGrounded()
    {
        Debug.DrawRay(Owner.transform.position + Owner.Collider.center, Vector3.down, Color.red);
        return Physics.SphereCast(Owner.transform.position + point2, Owner.Collider.radius, Vector3.down, out  GroundHitInfo, Owner.GroundCheckDistance + Owner.SkinWidth, Owner.WalkableMask);

    }
    #endregion





    #region Key Items
    private void HandleFlashLight()
    {

        if (GameManager.instance.HasFlashlight)
        {
            if (Input.GetKeyDown(KeyCode.F) && Owner.Flashlight.enabled == false)
            {
                Owner.Flashlight.enabled = true;
                PlayerPrefs.SetInt("FlashlightEnabled", 1);
            }
            else if (Input.GetKeyDown(KeyCode.F) && Owner.Flashlight.enabled)
            {
                Owner.Flashlight.enabled = false;
                PlayerPrefs.SetInt("FlashlightEnabled", 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.instance.Pause();
        }
    }
    #endregion




}
