using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float hitPoints = 100f;
    [SerializeField] private float movementSpeed = 5.0f;

    private Material material;

    private Transform player;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
        player = FindObjectOfType<Player>().transform;
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
        Vector3 direction = (player.position - transform.position).normalized;
        direction = new Vector3(direction.x, 0, direction.z).normalized;
        transform.position += direction * movementSpeed * Time.deltaTime;
    }

    public void Damage (float speed, float damage)
    {
        material.color = Color.red * hitPoints/100;
        Debug.Log((speed * damage)/10);
        hitPoints -= (speed * damage)/10;
    }
}
