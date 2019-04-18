using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    public float attackDamage = 25f;
    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 5.0f;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public BoxCollider boxCollider;
    [HideInInspector] public GameObject attackHitbox;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        attackHitbox = transform.GetChild(0).gameObject;
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
