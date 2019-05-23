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
    protected bool isGrounded;

    private float speed;
    private bool canStand;
    private bool isCrouching;

    //Collision Check
    private Vector3 point1;
    protected Vector3 point2;
    private Vector3 snapSum;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;

    public override void Initialize (StateMachine owner)
    {
        this.Owner = (Player)owner;
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

        CheckCollision();

        Move();
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
        Direction = cameraRotation * Direction;

        if (Physics.Raycast(Owner.transform.position, Vector3.down, out RaycastHit hitInfo)) {
            Direction = Vector3.ProjectOnPlane(Direction, hitInfo.normal).normalized;
        }
        else {
            Direction = Vector3.ProjectOnPlane(Direction, Vector3.up).normalized;
        }
    }

    private void Move()
    {
        Owner.transform.position += Owner.Velocity * Time.deltaTime - snapSum;
    }

    private void ResetValues()
    {
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }
    #endregion

    #region Physics
    private void ApplyGravity()
    {
        Owner.Velocity -=  Vector3.up * Owner.Gravity * Time.deltaTime;
    }

    private void ApplyAirResistance()
    {
        Owner.Velocity *= Mathf.Pow(Owner.AirResistanceCoefficient, Time.deltaTime);
    }

    private void ApplyFriction(float normalForceMagnitude)
    {
        if (Owner.Velocity.magnitude < CalculateStaticFriction(normalForceMagnitude))
        {
            Owner.Velocity = Vector3.zero;
        }
        else
        {
            Owner.Velocity += -Owner.Velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);
        }
    }

    private float CalculateStaticFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, Owner.StaticFrictionCoefficient);
    }

    private float CalculateDynamicFriction(float normalForceMagnitude)
    {
        return Functions.CalculateFriction(normalForceMagnitude, Owner.DynamicFrictionCoefficient);
    }
    #endregion

    #region Collision/Groundcheck
    private void CheckCollision()
    {
        point1 = Owner.Collider.center + Vector3.up * ((Owner.Collider.height / 2) - Owner.Collider.radius);
        point2 = Owner.Collider.center + Vector3.down * ((Owner.Collider.height / 2) - Owner.Collider.radius);
        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter)
        {
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(Owner.transform.position + point1, Owner.transform.position + point2, Owner.Collider.radius, Owner.Velocity.normalized, out hitInfo, Owner.Velocity.magnitude * Time.deltaTime + Owner.SkinWidth, Owner.WalkableMask))
            {
                float impactAngle = 90 - Vector2.Angle(Owner.Velocity.normalized, hitInfo.normal);
                float hypotenuse = Owner.SkinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

                if (hitInfo.distance > Mathf.Abs(hypotenuse))
                {
                    snapSum += Owner.Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    Owner.transform.position += Owner.Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                }

                Vector3 normalForce;
                normalForce = Functions.CalculateNormalForce(Owner.Velocity, hitInfo.normal);

                Owner.Velocity += normalForce;
                //Debug.Log("Owner.Velocity: " + Owner.Velocity);

                //ApplyFriction(normalForce.magnitude);

                CheckCollision();

            }
        }

    }

    public void GroundCheck()
    {
        if(Physics.SphereCast(Owner.transform.position + point2, Owner.Collider.radius, Vector3.down, out RaycastHit groundHitInfo, Owner.GroundCheckDistance + Owner.SkinWidth, Owner.WalkableMask))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    public bool IsGrounded()
    {
        Debug.DrawRay(Owner.transform.position + Owner.Collider.center, Vector3.down, Color.red);
        return Physics.SphereCast(Owner.transform.position + point2, Owner.Collider.radius, Vector3.down, out RaycastHit groundHitInfo, Owner.GroundCheckDistance + Owner.SkinWidth, Owner.WalkableMask);

    }
    #endregion

    #region Inventory
    private void HandleFlashLight()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Owner.HasFlashlight)
            {
                if (Owner.Flashlight.enabled)
                {
                    Owner.Flashlight.enabled = false;
                }
                else
                {
                    Owner.Flashlight.enabled = true;
                }
            }
        }
    }
    #endregion

    protected void SetVelocity ()
    {
        //Debug.Log("Stamina: " + Owner.Stamina.Stamina);
        if (Input.GetKey(KeyCode.LeftShift) && Owner.Stamina.Stamina > 0 && isCrouching == false) {
            speed = Owner.SprintSpeed;
            Owner.Stamina.UseStamina();
        }
        else {
            Owner.Stamina.RecoverStamina();
            canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;

            if (Input.GetKey(KeyCode.LeftControl)) {
                isCrouching = true;
                Owner.CrouchSetup();
                //canStand = Physics.Raycast(Owner.transform.position, Vector3.up, Owner.CrouchMargin, Owner.WalkableMask) == false;
                speed = Owner.CrouchSpeed;
            }
            else if (Input.GetKey(KeyCode.LeftControl) == false && isCrouching && canStand == false) {
                speed = Owner.CrouchSpeed;

            }
            else if (Input.GetKey(KeyCode.LeftControl) == false && isCrouching && canStand) {

                Owner.NormalSetup();
                speed = Owner.WalkSpeed;
                isCrouching = false;
            }
            else {
                speed = Owner.WalkSpeed;
                isCrouching = false;
            }


        }
        //Owner.Velocity += Direction * Owner.Acceleration * Time.deltaTime;
        Owner.Velocity = new Vector3(Direction.x * speed, Owner.Velocity.y, Direction.z * speed);
    }

    private void CameraRotation()
    {
        var cameraRotation = Owner.mainCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        Owner.transform.rotation = cameraRotation;
        //Owner.GravityGunObject.transform.rotation = cameraRotation;
        cameraRotation = Owner.mainCamera.transform.rotation;
    }



}
