using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    public float attackDistance = 2.0f;

    public float attackDamage = 25f;
    public float attackTime = 1.0f;
    public float attackCooldown = 1.0f;
    public float attackAnimationSpeed = 2.0f;
    [HideInInspector] public bool attacking = false;

    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float acceleration = 10.0f;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public BoxCollider boxCollider;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
