using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{


    //Attributes

    public float groundCheckDistance;
    public GameObject mainCamera;
    public LayerMask walkableMask;
    public float skinWidth;
    [HideInInspector] public CapsuleCollider capsuleCollider;
    [HideInInspector] public PhysicsComponent physics;
    public GravityGun gravityGun;
    private Transform healthBar;

    public GameObject respawnPoint;

    public float groundAcceleration;
    //public float airAcceleration;
    public float jumpHeight;
    public float startHealth;
    [HideInInspector] public float health;


    protected override void Awake()
    {
        health = startHealth;
        physics = GetComponent<PhysicsComponent>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        healthBar = transform.GetChild(2);
        base.Awake();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Damage(100);
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
        transform.position = respawnPoint.transform.position;
    }

}
