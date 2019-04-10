using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    [SerializeField] private float movementSpeed = 5.0f;

    private Transform player;

    void Awake()
    {
        player = FindObjectOfType<PlayerController3D>().transform;
    }

    void Update()
    {
        MoveTowardPlayer();

        if(hitPoints < 0) {
            Debug.Log("Kill enemy");
            Destroy(gameObject);
        }
    }

    private void MoveTowardPlayer ()
    {
        transform.position += (player.position - transform.position).normalized * movementSpeed * Time.deltaTime;
    }

    public void Damage (float speed, float damage)
    {
        Debug.Log((speed * damage)/10);
        hitPoints -= (speed * damage)/10;
    }
}
