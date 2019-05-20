//Author Simon Sundström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float acceptableStamina = 15;
    [SerializeField] private float margin;

    [Header("Movement")]
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float crouchSpeed;
    [Range(0, 1)][Tooltip("Slow down the movementspeed by a precentage when player is moving backwards")]
    [SerializeField] private float backwardsSpeedModifier;
    [SerializeField] private float gravityModifier;
    [SerializeField] private float jumpForce;
    [SerializeField] private float fallGravityScale;
    [SerializeField] private float jumpGravityScale;
    [SerializeField] private LayerMask walkableMask;
    [SerializeField] private float skinWidth = 0.05f;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private float gravity;
    [SerializeField] private KeyCode crouchKey;

    private float speed;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector3 verticalVelocity;
    private bool isGrounded;
    private bool playerJumping;
    private bool isCrouching;
    private bool canStand;

    //Crouch
    private float CrouchColliderHeight = 0.80f;
    private float CrouchColliderCenter = 0.40f;
    private float CrouchCameraHeight = 0.5f;

    private Camera playerCamera;

    public PlayAudioMessage PlayAudioMessage { get; private set; } 

    //For Collision
    private Vector3 point1;
    private Vector3 point2;
    private Vector3 snapSum;
    [HideInInspector] public CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;
    private float groundAngle;
    
    [HideInInspector] public Light flashlight;
    public bool hasFlashlight = false;

    private PlayerStates states;
    private StaminaComponent staminaComponent;

    float forward;
    float horizontal;


    private void Awake()
    {
        GameManager.Instance.CurrentCheckPoint = transform.position;
        PlayAudioMessage = GetComponent<PlayAudioMessage>();
        flashlight = GetComponentInChildren<Light>();

        if (GameManager.Instance.RestartedFromLatestCheckpoint)
        {
            transform.position = GameManager.Instance.CurrentCheckPoint;
        }
        capsuleCollider = GetComponent<CapsuleCollider>();
        playerCamera = Camera.main;
        staminaComponent = GetComponent<StaminaComponent>();
    }



    // Update is called once per frame
    void Update()
    {
        HandleInput();
        HandleMovement();
        HandleCameraRotation();
        HandleFlashLight();

    }



    private void HandleInput()
    {
  
        forward = Input.GetAxisRaw("Vertical");
        horizontal = Input.GetAxisRaw("Horizontal");
        direction = new Vector3(horizontal, 0, forward);
        Quaternion cameraRotation = playerCamera.transform.rotation;
        direction = cameraRotation * direction;
        direction = GetGroundDirection();
    }

    private void HandleMovement()
    {

        if (IsGrounded())
        {
            switch (states)
            {
                case PlayerStates.WALK:
                    WalkState();

                    break;
                case PlayerStates.SPRINT:
                    SprintState();
                    break;
                case PlayerStates.CROUCH:
                    CrouchState();
                    break;

            }

            velocity = direction.normalized * speed;

            //CheckIfMovingBackwards();

            playerJumping = false;

            if (Input.GetAxisRaw("Jump") > 0f)
            {
                Jump();

            }
        }
        else
        {
        
            if (playerJumping)
            {
                FallFromJump();

            }
            else
            {
                FallFromGround();
            }
        }

        Move();

    }

    private void Jump()
    {
        playerJumping = true;

        verticalVelocity = new Vector3(0, jumpForce, 0);

        velocity = direction.normalized * speed + verticalVelocity;
    }

    private void FallFromGround()
    {
        float yValue = velocity.y + gravity * fallGravityScale * Time.deltaTime;
        verticalVelocity = new Vector3(0f, yValue, 0f);
        direction.y = 0;
        velocity = direction * speed + verticalVelocity;
    }

    private void FallFromJump()
    {
        float yValue = velocity.y + gravity * jumpGravityScale * Time.deltaTime;
        verticalVelocity = new Vector3(0f, yValue, 0f);
        direction.y = 0;

        velocity = direction * speed + verticalVelocity;
    }

    private void SprintState()
    {
        speed = sprintSpeed;
        if (forward < 0) {
            speed *= backwardsSpeedModifier;
        }
        staminaComponent.UseStamina();

        if (Input.GetKey(KeyCode.LeftShift) == false || staminaComponent.Stamina <= 0)
        {
            states = PlayerStates.WALK;
        }
    }

    private void CrouchState()
    {
        CrouchSetup();
        staminaComponent.RecoverStamina();
        canStand = Physics.Raycast(transform.position, Vector3.up, margin, walkableMask) == false;
        speed = crouchSpeed;
        if ((Input.GetKeyDown(crouchKey) && canStand))
        {
            states = PlayerStates.WALK;

            NormalSetup();

        }
    }

    private void WalkState()
    {
        speed = walkSpeed;
        if (forward < 0) {
            speed *= backwardsSpeedModifier;
        }
    
        staminaComponent.RecoverStamina();


        if (Input.GetKeyDown(crouchKey))
        {
            states = PlayerStates.CROUCH;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && staminaComponent.Stamina >= acceptableStamina)
        {
            states = PlayerStates.SPRINT;
        }
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

    //private void CheckIfMovingBackwards ()
    //{
    //    if (Vector3.Dot(velocity, transform.forward) <= -0.9f) {
    //        velocity *= backwardsSpeedModifier;
    //    }
    //}

    public bool IsGrounded()
    {
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit groundHitInfo, groundCheckDistance + skinWidth, walkableMask);
    }


    public Vector3 GetGroundDirection()
    {
        //Du vill bara kolla det här på marken
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo) && IsGrounded())
        {

            return Vector3.ProjectOnPlane(direction, hitInfo.normal).normalized;

        }
        else
        {
            return Vector3.ProjectOnPlane(direction, Vector3.down).normalized;
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

                CheckCollision();

            }
        }

    }

    private void HandleFlashLight()
    {
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

    private void HandleCameraRotation()
    {
        var cameraRotation = playerCamera.transform.rotation;
        cameraRotation.z = 0;
        cameraRotation.x = 0;
        transform.rotation = cameraRotation;
        cameraRotation = playerCamera.transform.rotation;
    }

    private Vector3 CalculateNormalForce(Vector3 velocity, Vector3 normal)
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

public enum PlayerStates
{
    WALK,
    CROUCH,
    SPRINT,
    AIR,
    GROUND
    
}
