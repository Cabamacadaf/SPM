﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastRadius : MonoBehaviour
{
    private GravityBlast gravityBlast;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask wallLayer;
    private void Awake ()
    {
        gravityBlast = GetComponentInParent<GravityBlast>();
    }
    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Enemy")) {
            Debug.DrawLine(firePoint.position, other.transform.position, Color.yellow, 2);
            Vector3 raycastDirection = firePoint.position - other.transform.position;
            if (!Physics.Raycast(other.transform.position, raycastDirection.normalized, out RaycastHit hit, raycastDirection.magnitude, wallLayer)) {
                Enemy enemy = other.GetComponent<Enemy>();
                enemy.Transition<EnemyBlastedState>();
                enemy.rigidBody.AddForce((enemy.transform.position - enemy.player.transform.position).normalized * gravityBlast.blastForce);
            }
        }
    }
}