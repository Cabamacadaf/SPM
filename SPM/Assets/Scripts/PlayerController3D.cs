using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    //Attributes


    //Walk
    [SerializeField] private float acceleration;

    private Vector3 direction;
    private float distance;

    public GameObject mainCamera;


    //Jump
    private Vector2 jumpForce;
    [SerializeField] private float jumpHeight;
    public float groundCheckDistance;


    //Other
    public LayerMask layerMask;
    [SerializeField] private float skinWidth;
    private float size;
    private CapsuleCollider capsuleCollider;
    private Vector3 snapSum;
    private Vector3 point1;
    private Vector3 point2;
    private RaycastHit hitInfo;


    //Collision
    private int checkCollisionCounter = 0;
    [SerializeField] private int maxLoopValue;

    private PhysicsComponent physics;
    // Start is called before the first frame update



    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        physics = GetComponent<PhysicsComponent>();

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        HandleInput();



        CheckCollision();

        transform.position += physics.GetVelocity() * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;

    }

    private void HandleInput()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = mainCamera.transform.rotation * input;

        if (Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out hitInfo, groundCheckDistance + skinWidth, layerMask))
        {
            input = Vector3.ProjectOnPlane(input, hitInfo.normal).normalized;

        }
        else
        {
            input = Vector3.ProjectOnPlane(input, Vector3.up).normalized;

        }


        physics.AddVelocity(input * acceleration * Time.deltaTime);

    }

    private void CheckCollision()
    {

        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter)
        {

            Vector3 velocity = physics.GetVelocity();
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, layerMask))
            {

                float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);

                float hypotenuse = skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);



                if (hitInfo.distance > Mathf.Abs(hypotenuse))
                {
                    snapSum += (Vector3)velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    transform.position += (Vector3)velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));

                }


                Vector2 normalForce = Functions.CalculateNormalForce(velocity, hitInfo.normal);
                physics.AddVelocity(normalForce);

                physics.ApplyFriction(normalForce.magnitude);

                CheckCollision();



            }
        }

    }
}
