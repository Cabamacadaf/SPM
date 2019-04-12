using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : StateMachine
{


    //Attributes

    public float groundCheckDistance;
    public GameObject mainCamera;
    public LayerMask layerMask;
    public float skinWidth;
    [HideInInspector] public CapsuleCollider capsuleCollider;
    [HideInInspector] public PhysicsComponent physics;
    public GravityGun gravityGun;
    private Transform healthBar;

    public GameObject respawnPoint;
    public float startHealth;
    private float health;


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
        Debug.Log("Player takes " + damage + " damage.");
        Debug.Log("Player has " + health + " health.");
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
    }

    public void Respawn()
    {
        Debug.Log("Player respawned");
        health = startHealth;
        healthBar.localScale = new Vector3(healthBar.localScale.x, health / 100, healthBar.localScale.z);
        transform.position = respawnPoint.transform.position;
    }

}
