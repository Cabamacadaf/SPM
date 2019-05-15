using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{

    public float CrouchColliderHeight = 0.80f;
    public float CrouchColliderCenter = 0.40f;
    public float CrouchCameraHeight;
    public float CrouchGravityGunHeight;

    public StaminaComponent Stamina { get; set; }
    public PlayerMovement Movement { get; set; }
    public float WalkSpeed { get => walkSpeed; set => walkSpeed = value; }
    public float JumpHeight { get => jumpHeight; set => jumpHeight = value; }
    public float Acceleration { get => acceleration; set => acceleration = value; }
    public float CrouchSpeed { get => crouchSpeed; set => crouchSpeed = value; }
    public float Deceleration { get => deceleration; set => deceleration = value; }
    public float RunningSpeed { get => runningSpeed; set => runningSpeed = value; }

    public GameObject mainCamera;
    public GravityGun gravityGun;
    private Transform healthBar;
    [HideInInspector] public Light flashlight;
    /*[HideInInspector]*/ public bool hasFlashlight = false;

    [SerializeField] private float walkSpeed = 8;
    [SerializeField] private float jumpHeight = 8;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float crouchSpeed = 10;
    [SerializeField] private float deceleration = 10;
    [SerializeField] private float runningSpeed = 25;



    public Transform respawnPoint;

    public float startHealth;
    [HideInInspector] public float health;
    [HideInInspector] public CapsuleCollider Collider;


    protected override void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        Collider = GetComponent<CapsuleCollider>();
        Movement = GetComponent<PlayerMovement>();
        Stamina = GetComponent<StaminaComponent>();
        health = startHealth;
        flashlight = GetComponentInChildren<Light>();
        healthBar = transform.GetChild(2);
        base.Awake();
        //if (GameManager.Instance.RestartedFromLatestCheckpoint)
        //{
        //    RespawnCheckpoint();
        //}

    }



}
