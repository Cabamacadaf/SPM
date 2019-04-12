using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    public float chaseDistance = 6.0f;
    public float attackDistance = 2.0f;

    public float attackDamage = 25f;
    public float attackTime = 1.0f;
    public float attackCooldown = 1.0f;
    public float attackAnimationSpeed = 2.0f;
    public bool attacking = false;

    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
