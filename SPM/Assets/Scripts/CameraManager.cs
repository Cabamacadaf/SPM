using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float rotationX;
    private float rotationY;

    [SerializeField] private float minClampValue;
    [SerializeField] private float maxClampValue;

    [SerializeField] private float zoomSensitivity;
    private float mouseSensitivity;
    [SerializeField] private float sensitivity;


    private Vector3 movement;

    private Vector3 cameraOffset = new Vector3(0, 2.0f, -7.0f);
    private SphereCollider sphereCollider;
    public LayerMask geometryLayer;


    enum State
    {
        STATE_THIRDPERSON,
        STATE_FIRSTPERSON,
        STATE_ZOOMED

    }

    private State state;

    // Start is called before the first frame update
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        state = State.STATE_THIRDPERSON;

    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        HandleUpdate();

        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;


        transform.rotation = Quaternion.Euler(Mathf.Clamp(rotationX, minClampValue, maxClampValue), rotationY, 0);

        movement = transform.rotation * cameraOffset;


        CheckCollision();
        transform.position = movement + transform.parent.position;
    }

    private void HandleUpdate()
    {
        switch (state)
        {
            case State.STATE_THIRDPERSON:
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    state = State.STATE_FIRSTPERSON;
                }
                minClampValue = -45;
                maxClampValue = 45;
                cameraOffset = new Vector3(0, 2.0f, -7.0f);
                mouseSensitivity = sensitivity;
                break;
            case State.STATE_FIRSTPERSON:
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    state = State.STATE_ZOOMED;
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = State.STATE_THIRDPERSON;
                }
                minClampValue = -90;
                maxClampValue = 90;
                mouseSensitivity = sensitivity;
                cameraOffset = new Vector3(0, 0, 0);
                break;
            case State.STATE_ZOOMED:
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    state = State.STATE_FIRSTPERSON;
                }
                else if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = State.STATE_THIRDPERSON;
                }
                minClampValue = -90;
                maxClampValue = 90;
                cameraOffset = new Vector3(0, 0, 7);
                mouseSensitivity = zoomSensitivity;
                break;
        }
    }

    private void CheckCollision()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.parent.position, sphereCollider.radius, movement.normalized, out hitInfo, movement.magnitude + sphereCollider.radius, geometriLayer))
        {
            movement = movement.normalized * (hitInfo.distance - sphereCollider.radius);
        }

    }
}
