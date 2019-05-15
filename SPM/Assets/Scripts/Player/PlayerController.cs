//Author Simon Sundström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float margin;


    //Speed
    private float speed;
    public float sprintSpeed;
    public float walkSpeed;
    public float gravityModifier;
    //Jumpheight
    public float jumpForce;
    //Rigidbody
    private Camera playerCamera;
    public float fallGravityScale;
    public float jumpGravityScale;
    //Direction
    private Vector3 direction;
    private float vertical;
    private float horizontal;
    public float gravity;
    private Vector3 velocity;
    private Vector3 verticalVelocity;
    private bool isGrounded;
    private bool playerJumping;

    [SerializeField] private LayerMask walkableMask;
    [SerializeField] float skinWidth = 0.05f;
    [SerializeField] float groundCheckDistance = 0.05f;

    private bool isCrouching;
    private bool canStand;

    private Vector3 point1;
    private Vector3 point2;
    private Vector3 snapSum;
    public CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;
    private float groundAngle;
    
    [HideInInspector] public Light flashlight;
    /*[HideInInspector]*/
    public bool hasFlashlight = false;
    public float startHealth;
    [HideInInspector] public float health;
    public Transform respawnPoint;
    //private MyCharachterController CC;

    public float CrouchColliderHeight = 0.80f;
    public float CrouchColliderCenter = 0.40f;
    public float CrouchCameraHeight;

    private void Awake()
    {
        flashlight = GetComponentInChildren<Light>();

        if (GameManager.Instance.RestartedFromLatestCheckpoint)
        {
            transform.position = GameManager.Instance.CurrentCheckPoint;
        }
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerCamera = Camera.main;



    }


    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;

        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);

        //CC = GetComponent<MyCharachterController>();

    }


    private void CameraRotation()
    {
        var cameraRotation = playerCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        transform.rotation = cameraRotation;
        cameraRotation = playerCamera.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();
        //HandleMovement();
        HandleMovement();
        CameraRotation();



        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasFlashlight)
            {
                if (flashlight.enabled)
                {
                    flashlight.enabled = false;
                }
                else
                {
                    flashlight.enabled = true;
                }
            }
        }

    }


    private void HandleInput()
    {
  
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        direction = new Vector3(horizontal, 0, vertical);
        Quaternion cameraRotation = playerCamera.transform.rotation;
        direction = cameraRotation * direction;
        direction = GetGroundDirection();
    }

    private void HandleMovement()
    {
        canStand = Physics.Raycast(transform.position, Vector3.up, margin, walkableMask);

        //Debug.Log("IsCrouching: " + isCrouching);
        //Debug.Log("Can Stand: " + canStand);
        //Vector3 movingDirection = GetDirection();


        if (Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.LeftControl))
        {
            CrouchSetup();
        }

        else
        {
           
            if (isCrouching)
            {
               
                if(canStand == false)
                {
                    isCrouching = false;
                    NormalSetup();
                }
  
          
            }
            else
            {
                isCrouching = false;
                NormalSetup();

            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (IsGrounded())
        {
            //Debug.Log(IsGrounded());
            //Vector3 movingDirection = MoveAlongGround(direction);
            velocity = direction.normalized * speed;
            playerJumping = false;

            if (Input.GetAxisRaw("Jump") > 0f)
            {
                playerJumping = true;

                verticalVelocity = new Vector3(0, jumpForce, 0);

                velocity = direction.normalized * speed + verticalVelocity;

            }

        }
        else
        {
            if (playerJumping)
            {

                verticalVelocity = new Vector3(0f, (velocity.y + (gravity * jumpGravityScale * Time.deltaTime)), 0f);

                direction.y = 0;

                velocity = direction * speed + verticalVelocity;

            }
            else
            {
                verticalVelocity = new Vector3(0f, (velocity.y + (gravity * fallGravityScale * Time.deltaTime)), 0f);
                direction.y = 0;
                velocity = direction * speed + verticalVelocity;
            }

        }

        //CC.Move(velocity);

        Move();

        //velocity.y = 0;

    }

    private void NormalSetup()
    {
        capsuleCollider.center = new Vector3(0, 0.93f, 0);
        capsuleCollider.height = 1.86f;
        GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, 1.7f, 0);
    }

    private void CrouchSetup()
    {
        capsuleCollider.center = new Vector3(0, CrouchColliderCenter, 0);
        capsuleCollider.height = CrouchColliderHeight;
        GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, CrouchCameraHeight, 0);
        isCrouching = true;
    }

    private void Move()
    {
        CheckCollision();

        transform.position += velocity * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }



    public bool IsGrounded()
    {
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit groundHitInfo, groundCheckDistance + skinWidth, walkableMask);
    }


    public Vector3 GetGroundDirection()
    {
        //Du vill bara kolla det här på marken
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo))
        {

            return Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }
        else
        {
            return direction.normalized;
        }

    }

    private void CheckCollision()
    {
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
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
                normalForce = CalculateNormalForce(velocity, hitInfo.normal);

                velocity += normalForce;

                //ApplyFriction(normalForce.magnitude);

                CheckCollision();

            }
        }

    }

   


    public Vector3 CalculateNormalForce(Vector3 velocity, Vector3 normal)
    {
        float dotProduct = Vector3.Dot(velocity, normal);

        if (dotProduct > 0)
        {
            dotProduct = 0;
        }

        Vector3 projection = dotProduct * normal;

        return -projection;
    }




}
