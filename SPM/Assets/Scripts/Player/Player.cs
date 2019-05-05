using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{
    //Attributes

    public GameObject mainCamera;
    public GravityGun gravityGun;
    private Transform healthBar;
    [HideInInspector] public Light flashlight;

    public Transform respawnPoint;

    public float startHealth;
    [HideInInspector] public float health;
    [HideInInspector] public PlayerMovement Movement;
    [HideInInspector] public CapsuleCollider Collider;


    protected override void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Collider = GetComponent<CapsuleCollider>();
        Movement = GetComponent<PlayerMovement>();
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

    public void Addhealth()
    {
        health = startHealth;
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

}
