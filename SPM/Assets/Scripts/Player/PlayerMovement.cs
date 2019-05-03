using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : PhysicsComponent
{
    public LayerMask walkableMask;

    public float JumpHeight;
    public float Acceleration;
    public float SkinWidth;
    public float GroundCheckDistance;
    public int maxLoopValue;

    private Vector3 direction;
    private Vector3 point1;
    public Vector3 point2;
    private Vector3 snapSum;

    public CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;

    public float stamina = 100;
    public int WalkingSpeed = 20;
    public int RunningSpeed = 40;
    public float RecoveryRate = 1f;
    public float LoseStamingRate = 1f;
    public const int FULL_STAMINA = 100;

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
        CheckCollision();
        //ApplyAirResistance();
        Debug.Log("Velocity: " + GetVelocity().magnitude);
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
            if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + SkinWidth, walkableMask))
            {
                float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);
                float hypotenuse = SkinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

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
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out hitInfo, GroundCheckDistance + SkinWidth, walkableMask);
    }

    public void Recover()
    {
        stamina += RecoveryRate * Time.deltaTime;
        if (stamina >= FULL_STAMINA)
        {
            stamina = FULL_STAMINA;
        }

    }

    public void Running()
    {
        stamina -= LoseStamingRate * Time.deltaTime;
        if(stamina <= 0)
        {
            stamina = 0;
        }
    }
}
