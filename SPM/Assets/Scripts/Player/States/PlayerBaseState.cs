//Author Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    //Attributes
    protected Player Owner;
    protected Vector3 Direction { get; set; }
    protected Vector3 Velocity;
    protected float SkinWidth { get; private set; }
    protected bool isGrounded;

    //Collision Check
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 snapSum;
    private CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;

    public override void Initialize (StateMachine owner)
    {
        this.Owner = (Player)owner;

        capsuleCollider = owner.GetComponent<CapsuleCollider>();
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
    }

    public override void HandleUpdate ()
    {
        GroundCheck();
        //Debug.Log("Is grounded: " + isGrounded);
        //Debug.Log("Velocity Normalized: " + Velocity.normalized);
        HandleInput();
        ApplyGravity();

        CheckCollision();
        //ApplyAirResistance();

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

    }

    private void Move()
    {
        Owner.transform.position += Velocity * Time.deltaTime - snapSum;
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
        Velocity.y -= Owner.Gravity * Time.deltaTime;
    }

    private void ApplyAirResistance()
    {
        Velocity *= Mathf.Pow(Owner.AirResistanceCoefficient, Time.deltaTime);
    }

    private void ApplyFriction(float normalForceMagnitude)
    {
        if (Velocity.magnitude < CalculateStaticFriction(normalForceMagnitude))
        {
            Velocity = Vector3.zero;
        }
        else
        {
            Velocity += -Velocity.normalized * CalculateDynamicFriction(normalForceMagnitude);
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
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter)
        {
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(Owner.transform.position + point1, Owner.transform.position + point2, capsuleCollider.radius, Velocity.normalized, out hitInfo, Velocity.magnitude * Time.deltaTime + Owner.SkinWidth, Owner.WalkableMask))
            {
                float impactAngle = 90 - Vector2.Angle(Velocity.normalized, hitInfo.normal);
                float hypotenuse = Owner.SkinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

                if (hitInfo.distance > Mathf.Abs(hypotenuse))
                {
                    snapSum += Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    Owner.transform.position += Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                }

                Vector3 normalForce;
                normalForce = Functions.CalculateNormalForce(Velocity, hitInfo.normal);

                Velocity += normalForce;
                //Debug.Log("Velocity: " + Velocity);

                //ApplyFriction(normalForce.magnitude);

                CheckCollision();

            }
        }

    }

    public void GroundCheck()
    {
        if(Physics.SphereCast(Owner.transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit groundHitInfo, Owner.GroundCheckDistance + Owner.SkinWidth, Owner.WalkableMask))
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
        return Physics.SphereCast(Owner.transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit groundHitInfo, Owner.GroundCheckDistance + Owner.SkinWidth, Owner.WalkableMask);

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
