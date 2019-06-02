//Author Simon Sundström
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    #region Properties
    public StaminaComponent Stamina { get; set; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public Vector3 Velocity { get; set; }
    public Camera mainCamera { get; set; }
    public float SkinWidth { get => skinWidth; set => skinWidth = value; }
    public LayerMask WalkableMask { get => walkableMask; set => walkableMask = value; }
    public float AirResistanceCoefficient { get => airResistanceCoefficient; set => airResistanceCoefficient = value; }
    public float StaticFrictionCoefficient { get => staticFrictionCoefficient; set => staticFrictionCoefficient = value; }
    public float DynamicFrictionCoefficient { get => dynamicFrictionCoefficient; set => dynamicFrictionCoefficient = value; }
    public float Gravity { get => gravity; set => gravity = value; }
    public float GroundCheckDistance { get => groundCheckDistance; set => groundCheckDistance = value; }
    public CapsuleCollider Collider { get; private set; }
    public bool HasFlashlight { get => hasFlashlight; set => hasFlashlight = value; }
    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float SprintSpeed { get => sprintSpeed; set => sprintSpeed = value; }
    public float CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    public float AcceptableStamina { get => acceptableStamina; set => acceptableStamina = value; }
    public GameObject GravityGunObject { get => gravityGun; set => gravityGun = value; }
    public PlayAudioMessage PlayVoiceLine { get; private set; }
    public float CrouchMargin { get => crouchMargin; set => crouchMargin = value; }
    public HealthComponent PlayerHealth { get; set; }
    public Light Flashlight { get; set; }
    

    #endregion

    #region Private Fields
    [Header("Movement")]
    [SerializeField] private float jumpHeight = 8;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float crouchSpeed;
    [SerializeField] private float acceptableStamina;
    [SerializeField] private float crouchMargin;


    [Header("Physics")]
    [SerializeField] private float airResistanceCoefficient;
    [SerializeField] private float staticFrictionCoefficient;
    [SerializeField] private float dynamicFrictionCoefficient;
    [SerializeField] private float gravity;

    [Header("Collision/Groundcheck")]
    [SerializeField] private float skinWidth;
    [SerializeField] private LayerMask walkableMask;
    
    [SerializeField] private float groundCheckDistance;

    [Header("Inventory")]
    [SerializeField] private bool hasFlashlight;
    [SerializeField] private GameObject gravityGun;
    #endregion

    private float acceleration = 10;
    private float CrouchColliderHeight = 0.80f;
    private float CrouchColliderCenter = 0.40f;
    private float CrouchCameraHeight = 0.5f;



    protected override void Awake()
    {
        PlayerHealth = GetComponent<HealthComponent>();
        Flashlight = GetComponentInChildren<Light>();
        GameManager.Instance.SetPlayer(gameObject);
        PlayVoiceLine = GetComponent<PlayVoiceLine>();

        Collider = GetComponent<CapsuleCollider>();
        Stamina = GetComponent<StaminaComponent>();
        mainCamera = Camera.main;

        LoadPlayer();

        base.Awake();
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if(data != null)
        {
            PlayerHealth.Health = data.health;

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];
            transform.position = position;

            Vector3 rotation;
            rotation.x = data.rotation[0];
            rotation.y = data.rotation[1];
            rotation.z = data.rotation[2];
            transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
        }



    }
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void NormalSetup()
    {
        Collider.center = new Vector3(0, 0.93f, 0);
        Collider.height = 1.86f;
        GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, 1.7f, 0);
    }

    public void CrouchSetup()
    {
        Collider.center = new Vector3(0, CrouchColliderCenter, 0);
        Collider.height = CrouchColliderHeight;
        GetComponentInChildren<LookY>().transform.localPosition = new Vector3(0, CrouchCameraHeight, 0);
    }
}
