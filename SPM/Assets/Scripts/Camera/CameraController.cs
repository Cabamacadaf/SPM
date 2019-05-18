//Main Author: Simon Sundström
//Secondary Author: Marcus Mellström


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("Maximum angle the player can look down")]
    [SerializeField] private float minClampValue;
    [Tooltip("Maximum angle the player can look up")]
    [SerializeField] private float maxClampValue;
    [Tooltip("Sets the speed the player can move around the camera")]
    [SerializeField] private float mouseSensitivity;
    [Tooltip("The CursorLockMode the player starts with." +
        "Confined: Pointers shows.\n" + "Locked: Pointer dosent show.")]
    [SerializeField] private CursorLockMode wantedMode;

    public bool MouseControlOn { get; set; }

    private float rotationX;
    private float rotationY;

    private void Awake ()
    {
        MouseControlOn = true;
        PlayerController player = FindObjectOfType<PlayerController>();
        rotationX = player.transform.eulerAngles.x;
        rotationY = player.transform.eulerAngles.y;
        LockCursor();
    }

    // Update is called once per frame
    void Update ()
    {
        CameraRotation();
    }

    ///<summary>
    ///Get and sets the rotations values from the mouse and rotates the camera
    ///</summary>
    private void CameraRotation ()
    {
        if (MouseControlOn == true) {
            rotationX -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
            rotationY += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        }

        ResetMouse();

        float rotationXCLamped = Mathf.Clamp(rotationX, minClampValue, maxClampValue);
        transform.rotation = Quaternion.Euler(rotationXCLamped, rotationY, 0);
    }

    ///<summary>
    ///Sets the rotation value to be equal to the clamp value in case the player move the mouse more then
    ///the minimum or maximum clampvalue
    ///</summary>
    private void ResetMouse ()
    {
        if (rotationX > maxClampValue) {
            rotationX = maxClampValue;
        }
        else if (rotationX < minClampValue) {
            rotationX = minClampValue;
        }
    }

    private void LockCursor ()
    {
        Cursor.lockState = wantedMode;
        Cursor.visible = false;
    }
}
