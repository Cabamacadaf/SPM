using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsComponent
{
    [SerializeField] private LayerMask walkableMask;
    [SerializeField] float skinWidth = 0.05f;
    [SerializeField] float groundCheckDistance = 0.05f;
    
    private Vector3 direction;
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 snapSum;
    private CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;
    private float groundAngle;

    private RaycastHit groundHitInfo;



    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
    }

    // Update is called once per frame
    void Update()
    {
        if (groundAngle >= 95 && groundAngle <= 85 && groundAngle != 0)
        {
            velocity = velocity.normalized * 5;

        }
        CalculateGroundAngle();
        Debug.Log(groundAngle);

        ApplyGravity();
        CheckCollision();
        ApplyAirResistance();


        transform.position += GetVelocity() * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }


    private void CheckCollision()
    {
        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter)
        {
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, walkableMask))
            {
                float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);
                float hypotenuse = skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

                if (hitInfo.distance > Mathf.Abs(hypotenuse))
                {
                    snapSum += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    transform.position += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                }


                Vector3 normalForce;
                normalForce = Functions.CalculateNormalForce(velocity, hitInfo.normal);

                AddVelocity(normalForce);

                ApplyFriction(normalForce.magnitude);

                CheckCollision();

            }
        }

    }

    public bool IsGrounded()
    {
      
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out groundHitInfo, groundCheckDistance + skinWidth, walkableMask);
    }


    public Vector3 MoveAlongGround(Vector3 direction)
    {
        if (Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit hitInfo, groundCheckDistance + skinWidth, walkableMask))
        {

            return Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }
        else
        {
            return direction.normalized;
        }
    }

    private void CalculateGroundAngle()
    {

        if (!IsGrounded())
        {
            groundAngle = 90;
        }
        groundAngle = Vector3.Angle(groundHitInfo.normal, transform.forward);
    }


    public void Decelerate()
    {

    }
}
