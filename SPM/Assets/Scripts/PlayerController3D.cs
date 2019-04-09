using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : PhysicsComponent
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
    private GravityGun gravityGun;
    private Vector3 snapSum;
    private Vector3 point1;
    private Vector3 point2;
    private RaycastHit hitInfo;


    //Collision
    private int checkCollisionCounter = 0;
    [SerializeField] private int maxLoopValue;

    // Start is called before the first frame update



    private void Awake ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        capsuleCollider = GetComponent<CapsuleCollider>();
        gravityGun = GetComponentInChildren<GravityGun>();
        point1 = capsuleCollider.center + Vector3.up * ((capsuleCollider.height / 2) - capsuleCollider.radius);
        point2 = capsuleCollider.center + Vector3.down * ((capsuleCollider.height / 2) - capsuleCollider.radius);
    }

    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {
        HandleInput();
        ApplyGravity();
        ApplyAirResistance();

        CheckCollision();

        transform.position += GetVelocity() * Time.deltaTime - snapSum;
        snapSum = Vector3.zero;
        checkCollisionCounter = 0;

    }

    private void HandleInput ()
    {

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = mainCamera.transform.rotation * input;

        if (Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out hitInfo, groundCheckDistance + skinWidth, layerMask)) {
            input = Vector3.ProjectOnPlane(input, hitInfo.normal).normalized;

        }
        else {
            input = Vector3.ProjectOnPlane(input, Vector3.up).normalized;

        }


        AddVelocity(input * acceleration * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && Physics.SphereCast(transform.position + point2, capsuleCollider.radius, Vector3.down, out hitInfo, groundCheckDistance + skinWidth, layerMask)) {
            AddVelocity(Vector3.up * jumpHeight);
        }

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Click");
            gravityGun.Shoot();
        }

    }

    protected override void CheckCollision ()
    {

        checkCollisionCounter++;
        if (maxLoopValue > checkCollisionCounter) {

            Vector3 velocity = GetVelocity();
            RaycastHit hitInfo;
            if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, velocity.normalized, out hitInfo, velocity.magnitude * Time.deltaTime + skinWidth, layerMask)) {
                //if (Physics.CapsuleCast(transform.position + point1, transform.position + point2, capsuleCollider.radius, -hitInfo.normal, velocity.magnitude * Time.deltaTime + skinWidth, layerMask)) {

                float impactAngle = 90 - Vector2.Angle(velocity.normalized, hitInfo.normal);

                float hypotenuse = skinWidth / Mathf.Sin(impactAngle * Mathf.Deg2Rad);



                if (hitInfo.distance > Mathf.Abs(hypotenuse)) {
                    snapSum += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
                    transform.position += velocity.normalized * (hitInfo.distance - Mathf.Abs(hypotenuse));
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


                AddVelocity(normalForce);

                if (hitInfo.collider.gameObject.CompareTag("MovingPlatform"))
                {

                    HandlePlatformCollision(normalForce.magnitude, hitInfo.collider.gameObject);

                }

                ApplyFriction(normalForce.magnitude);



                //}
                CheckCollision();

            }
        }

    }

    private void HandlePlatformCollision(float normalForceMagnitude, GameObject hit)
    {
        Vector3 horizontalVelocity = new Vector3(GetVelocity().x, 0, GetVelocity().z);
        Vector3 difference = horizontalVelocity - hit.GetComponent<Platform>().GetVelocity();
        if (difference.magnitude > CalculateStaticFriction(normalForceMagnitude))
        {
            transform.position +=(GetVelocity() - difference) * Time.deltaTime;
        }

    }
}
