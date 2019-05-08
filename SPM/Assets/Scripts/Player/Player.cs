using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
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
        Cursor.lockState = CursorLockMode.Locked;
        Collider = GetComponent<CapsuleCollider>();
        Movement = GetComponent<PlayerMovement>();
        Stamina = GetComponent<StaminaComponent>();
        health = startHealth;
        flashlight = GetComponentInChildren<Light>();
        healthBar = transform.GetChild(2);
        base.Awake();
        if (SceneController.Instance.RestartedFromLatestCheckpoint)
        {
            RespawnCheckpoint();
        }

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
        if(health > 100.0f) {
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

    public void RespawnCheckpoint()
    {
        transform.position = SceneController.Instance.lastCheckPointPos;

    }

    public float GetWalkSpeed()
    {
        return walkSpeed;
    }

}
