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
    [SerializeField] private float mouseSensitivity;


    private Vector3 movement;

    [SerializeField] private Vector3 cameraOffset;
    private SphereCollider sphereCollider;
    public LayerMask geometryLayer;



    // Start is called before the first frame update
    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();

    }
  
    // Update is called once per frame
    void Update()
    {

        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;

        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;


        transform.rotation = Quaternion.Euler(Mathf.Clamp(rotationX, minClampValue, maxClampValue), rotationY, 0);

        movement = transform.rotation * cameraOffset;


        CheckCollision();
        transform.position = movement + transform.parent.position;
    }



    private void CheckCollision()
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.parent.position, sphereCollider.radius, movement.normalized, out hitInfo, movement.magnitude + sphereCollider.radius, geometryLayer))
        {
            movement = movement.normalized * (hitInfo.distance - sphereCollider.radius);
        }

    }
}
