//Author: Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float rotationX;
    private float rotationY;

    [SerializeField] private float minClampValue;
    [SerializeField] private float maxClampValue;

    [SerializeField] private float mouseSensitivity;

    public GameObject ParentCamera;

    Vector3 movement;

    public Vector3 cameraOffset;
    private SphereCollider sphereCollider;
    public LayerMask geometryLayer;

    public static Vector3 FIRSTPERSON;
    public static Vector3 THIRDPERSON;



    // Start is called before the first frame update
    private void Awake()
    {
        FIRSTPERSON = new Vector3(0, 0.75f, 0);

        THIRDPERSON = new Vector3(cameraOffset.x, cameraOffset.y, cameraOffset.z);
        sphereCollider = GetComponent<SphereCollider>();

    }
  
    // Update is called once per frame
    void Update()
    {


        rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;

        if(rotationX > maxClampValue)
        {
            rotationX = maxClampValue;
        }
        else if(rotationX < minClampValue) 
        {
            rotationX = minClampValue;
        }

        transform.rotation = Quaternion.Euler(Mathf.Clamp(rotationX, minClampValue, maxClampValue), rotationY, 0);

        //förhållandet mellan karaktären och kameran, i den riktning kameran är roterad mot.
        Vector3 wantedMovement = transform.rotation * cameraOffset;

        
        movement = PreventCollision(wantedMovement);

      
        transform.position = movement + transform.parent.position;

        


    }

    private void LateUpdate()
    {
 

    }



    private Vector3 PreventCollision(Vector3 wantedMovement)
    {
        RaycastHit hitInfo;
        if (Physics.SphereCast(transform.parent.position, sphereCollider.radius, wantedMovement.normalized, out hitInfo, wantedMovement.magnitude + sphereCollider.radius, geometryLayer))
        {
            Debug.Log("Colliding");

            wantedMovement = wantedMovement.normalized * (hitInfo.distance - sphereCollider.radius);
        }
        return wantedMovement;
    }
}
