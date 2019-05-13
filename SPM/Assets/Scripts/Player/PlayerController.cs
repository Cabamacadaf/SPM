//Author Simon Sundström

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

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

    private Vector3 point1;
    private Vector3 point2;
    private Vector3 snapSum;
    public CapsuleCollider capsuleCollider;
    private int checkCollisionCounter = 0;
    private int maxLoopValue = 30;
    private float groundAngle;


    public GravityGun gravityGun;
    private Transform healthBar;
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
    public float CrouchGravityGunHeight;

    private void Awake()
    {
        flashlight = GetComponentInChildren<Light>();
        healthBar = transform.GetChild(2);
        if (SceneController.Instance.RestartedFromLatestCheckpoint)
        {
            RespawnCheckpoint();
        }


    }
    public void RespawnCheckpoint()
    {
        transform.position = SceneController.Instance.lastCheckPointPos;

    }

    public void Damage(float damage)
    {
        health -= damage;
        healthBar.localScale = new Vector3(healthBar.localScale.x, health / 100, healthBar.localScale.z);
        if (health <= 0)
        {
            Respawn();
        }
        else if (health <= 20)
        {
            //Blinka rött;
        }
    }

    public void Addhealth(float healthToAdd)
    {
        health += healthToAdd;
        if (health > 100.0f)
        {
            health = 100.0f;
        }
        healthBar.localScale = new Vector3(healthBar.localScale.x, health / 100, healthBar.localScale.z);
    }

    public void Respawn()
    {
        health = startHealth;
        healthBar.localScale = new Vector3(healthBar.localScale.x, health / 100, healthBar.localScale.z);
        transform.position = respawnPoint.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = Camera.main;

        capsuleCollider = GetComponent<CapsuleCollider>();
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
        gravityGun.transform.rotation = cameraRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleInput
        //HandleMovement();
        HandleMovement();
        CameraRotation();

    }

    private void FixedUpdate()
    {
       
    }

    private void HandleInput()
    {
        //Get Direction From KeyBoard

        //Set velocity to Direction * Speed * time.deltatime

        //CheckCollision

        //Move Player
    }

    private void HandleMovement()
    {
        Vector3 direction = GetDirection();
        Vector3 movingDirection = MoveAlongGround(direction);

        //Vector3 movingDirection = GetDirection();

        if (Input.GetKey(KeyCode.V))
        {
            capsuleCollider.center = new Vector3(0, CrouchColliderCenter, 0);
            capsuleCollider.height = CrouchColliderHeight;
            GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, CrouchCameraHeight, 0);
            gravityGun.transform.localPosition = new Vector3(0.44f, CrouchGravityGunHeight, 0.57f);
        }
        else
        {
            capsuleCollider.center = new Vector3(0, 0.93f, 0);
            capsuleCollider.height = 1.86f;
            GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, 1.7f, 0);
            gravityGun.transform.localPosition = new Vector3(0.44f, 1.34f, 0.57f);
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
            velocity = movingDirection.normalized * speed;
            playerJumping = false;

            if (Input.GetAxisRaw("Jump") > 0f)
            {
                playerJumping = true;

                verticalVelocity = new Vector3(0, jumpForce, 0);

                velocity = movingDirection.normalized * speed + verticalVelocity;

            }

        }
        else
        {
            if (playerJumping)
            {

                verticalVelocity = new Vector3(0f, (velocity.y + (gravity * jumpGravityScale * Time.deltaTime)), 0f);

                movingDirection.y = 0;

                velocity = movingDirection * speed + verticalVelocity;

            }
            else
            {
                verticalVelocity = new Vector3(0f, (velocity.y + (gravity * fallGravityScale * Time.deltaTime)), 0f);
                movingDirection.y = 0;
                velocity = movingDirection * speed + verticalVelocity;
            }

        }

        //CC.Move(velocity);

        MoveWithNormalForce();

        //velocity.y = 0;

    }

    private void MoveWithNormalForce()
    {
        CheckCollision();

        transform.position += velocity * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;
    }

    private void Move()
    {
        CheckCollision2();

    }

    private Vector3 GetDirection()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 keyboardDirection = new Vector3(horizontal, 0, vertical);
        Quaternion cameraRotation = playerCamera.transform.rotation;
        return cameraRotation * keyboardDirection;
    }

    private Vector3 GetKeyboardDirection()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        return new Vector3(horizontal, 0, forward).normalized;
    }

    public bool IsGrounded()
    {
        return Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out RaycastHit groundHitInfo, groundCheckDistance + skinWidth, walkableMask);
    }


    public Vector3 MoveAlongGround(Vector3 direction)
    {
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

    private void CheckCollision2()
    {

        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        RaycastHit hitInfo;
        if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, walkableMask))
        {


            float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);
            float hypotenuse = skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);

            if (hitInfo.distance > Mathf.Abs(hypotenuse))
            {
                transform.position += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
            }

            //Vector3 normalForce;
            //normalForce = CalculateNormalForce(velocity, hitInfo.normal);

            //velocity += normalForce;

            //ApplyFriction(normalForce.magnitude);

            // CheckCollision();


        }
        else
        {
            transform.position += velocity * Time.deltaTime;

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
