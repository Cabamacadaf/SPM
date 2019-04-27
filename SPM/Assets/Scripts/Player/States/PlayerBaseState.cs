using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseState : State
{
    //Attributes
    
    private Vector3 direction;
    private float distance;
    private float size;
    private Vector3  snapSum;
    private RaycastHit hitInfo;

    private int checkCollisionCounter = 0;
    [SerializeField] private int maxLoopValue;

    protected Player owner;

    protected Vector3 point1;
    protected Vector3 point2;


    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;

        point1 = owner.capsuleCollider.center + Vector3.up * ((owner.capsuleCollider.height / 2) - owner.capsuleCollider.radius);
        point2 = owner.capsuleCollider.center + Vector3.down * ((owner.capsuleCollider.height / 2) - owner.capsuleCollider.radius);

    }

    public override void Initialize(StateMachine owner)
    {
        this.owner = (Player)owner;
    }

  

    public override void HandleUpdate()
    {
        HandleInput();
        owner.physics.ApplyGravity();
        owner.physics.ApplyAirResistance();

        CheckCollision();

        owner.transform.position += owner.physics.GetVelocity() * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;

    }

    private void HandleInput()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = owner.mainCamera.transform.rotation * input;

        if (Physics.SphereCast(owner.transform.position + point2, owner.capsuleCollider.radius, Vector3.down, out hitInfo, owner.groundCheckDistance + owner.skinWidth, owner.walkableMask))
        {
            input = Vector3.ProjectOnPlane(input, hitInfo.normal).normalized;

        }
        else
        {
            input = Vector3.ProjectOnPlane(input, Vector3.up).normalized;

        }


        var cameraRotation = owner.mainCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        owner.transform.rotation = cameraRotation;
        cameraRotation = owner.mainCamera.transform.rotation;
        owner.gravityGun.transform.rotation = cameraRotation;

        owner.physics.AddVelocity(input * owner.groundAcceleration * Time.deltaTime);


        if (Input.GetMouseButtonDown(0))
        {
            owner.gravityGun.Push();
        }

        if (Input.GetMouseButtonDown(1))
        {
            owner.gravityGun.Pull();
        }

    }

    protected void CheckCollision()
    {

        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter)
        {

            Vector3 velocity = owner.physics.GetVelocity();
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(owner.transform.position + point1, owner.transform.position + point2, owner.capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + owner.skinWidth, owner.walkableMask))
            {
                //if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, -hitInfo.normal, velocity.magnitude * Time.deltaTime + skinWidth, layerMask)) {

                float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);

                float hypotenuse = owner.skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);



                if (hitInfo.distance > Mathf.Abs(hypotenuse))
                {
                    snapSum += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    owner.transform.position += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                }

                //transform.position += (Vector3)(-hitInfo.normal * (hitInfo.distance - skinWidth));
                //snapSum += (-hitInfo.normal * (hitInfo.distance - skinWidth));
                Vector3 normalForce;
                normalForce = Functions.CalculateNormalForce(velocity, hitInfo.normal);

                if (hitInfo.collider.gameObject.CompareTag("MovingPlatform"))
                {
                    Debug.Log(hitInfo.collider.GetComponent<Platform>().GetVelocity().y);
                    normalForce += new Vector3(0, hitInfo.collider.GetComponent<Platform>().GetVelocity().y, 0);


                }


                owner.physics.AddVelocity(normalForce);

                //if (hitInfo.collider.gameObject.CompareTag("MovingPlatform"))
                //{

                //    HandlePlatformCollision(normalForce.magnitude, hitInfo.collider.gameObject);

                //}

                owner.physics.ApplyFriction(normalForce.magnitude);



                //}
                CheckCollision();

            }
        }

    }

    


    //private void HandlePlatformCollision(float normalForceMagnitude, GameObject hit)
    //{
    //    Vector3 horizontalVelocity = new Vector3(GetVelocity().x, 0, GetVelocity().z);
    //    Vector3 difference = horizontalVelocity - hit.GetComponent<Platform>().GetVelocity();
    //    if (difference.magnitude > CalculateStaticFriction(normalForceMagnitude))
    //    {
    //        owner.transform.position += (GetVelocity() - difference) * Time.deltaTime;
    //    }

    //}
}
