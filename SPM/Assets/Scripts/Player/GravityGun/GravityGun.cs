//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GravityGun : StateMachine
{
    #region Private Fields
    [SerializeField] private float pullRange = 12.0f;
    [SerializeField] private float pushForce = 900f;
    [SerializeField] private float pullForce = 12.0f;
    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] private float distanceToDrop = 1.0f;
    [SerializeField] private float objectRotationSpeed = 2.0f;
    [SerializeField] private float rotationMouseSensitivity = 1.0f;

    [SerializeField] private LayerMask raycastCollideLayer;
    [SerializeField] private LayerMask pullLayer;

    [SerializeField] private Transform pullPoint;
    #endregion

    #region Properties
    public float PullRange { get => pullRange; private set => pullRange = value; }
    public float PushForce { get => pushForce; set => pushForce = value; }
    public float PullForce { get => pullForce; private set => pullForce = value; }
    public float DistanceToGrab { get => distanceToGrab; private set => distanceToGrab = value; }
    public float DistanceToDrop { get => distanceToDrop; private set => distanceToDrop = value; }
    public float ObjectRotationSpeed { get => objectRotationSpeed; private set => objectRotationSpeed = value; }
    public float RotationMouseSensitivity { get => rotationMouseSensitivity; set => rotationMouseSensitivity = value; }

    public bool IsRotated { get; set; }
    public Quaternion ObjectRotation { get; set; }

    public LayerMask RaycastCollideLayer { get => raycastCollideLayer; private set => raycastCollideLayer = value; }
    public LayerMask PullLayer { get => pullLayer; private set => pullLayer = value; }

    public Image Crosshair { get; private set; }

    public Transform PullPoint { get => pullPoint; private set => pullPoint = value; }
    public PickUpObject HoldingObject { get; set; }
    #endregion

    protected override void Awake ()
    {
        Crosshair = FindObjectOfType<Canvas>().transform.Find("Crosshair").GetComponent<Image>();
        base.Awake();
    }
}
