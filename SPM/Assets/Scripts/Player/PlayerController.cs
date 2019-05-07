//Author Simon Sundström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector3 Velocity { get; set; }

    [SerializeField] private float skinWidth;
    [SerializeField] private LayerMask walkableMask;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private float gravity = 9.82f;
    [SerializeField] private float maxGroundAngle = 120;
    //[SerializeField] private bool debug;
    [SerializeField] private float groundAngle;

    private Vector3 point1;
    private Vector3 point2;
    private CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue;
    private Vector3 snapSum;
    private RaycastHit groundHitInfo;
    private bool grounded;


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
        ApplyGravity();
        GroundCheck();
        CalculateGroundAngle();

        CheckCollision();

        if (groundAngle <= maxGroundAngle)
        {
            Move();
        }



        ResetValues();
    }

    public void Move()
    {
        transform.position += Velocity * Time.deltaTime - snapSum;
    }

    public void ApplyGravity()
    {
        if (!IsGrounded())
        {
            transform.position += Vector3.down * gravity * Time.deltaTime;

        }
    }

    private void CheckCollision()
    {
        if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, Velocity.normalized, out RaycastHit hitInfo, Velocity.magnitude * Time.deltaTime + skinWidth, walkableMask))
        {
            float impactAngle = 90 - Vector2.Angle(Velocity.normalized, hitInfo.normal);
            float hypotenuse = skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

            if (hitInfo.distance > Mathf.Abs(hypotenuse))
            {
                snapSum += Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                transform.position += Velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
            }

            
            Vector3 normalForce = Functions.CalculateNormalForce(Velocity, hitInfo.normal);

            Velocity = normalForce;

            CheckCollision();



        }


    }

    private void ResetValues()
    {
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }

    private void CalculateGroundAngle()
    {
   
        if (!IsGrounded())
        {
            groundAngle = 90;
        }
        groundAngle = Vector3.Angle(groundHitInfo.normal, transform.forward);
    }

    public Vector3 CalculateMoveDirection(Vector3 direction)
    {
      
         return  Vector3.ProjectOnPlane(direction, groundHitInfo.normal).normalized;

       
    }

    public bool IsGrounded()
    {
            
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out groundHitInfo, groundCheckDistance + skinWidth, walkableMask);
    }

    private void GroundCheck()
    {
        if(Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out groundHitInfo, groundCheckDistance + skinWidth, walkableMask)){
            grounded = true;
        }
        else
        {
            grounded = false;
        }

      
    }

 
}
