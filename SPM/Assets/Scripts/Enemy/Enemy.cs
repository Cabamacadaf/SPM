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

    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 5.0f;
    [HideInInspector] public MeshRenderer meshRenderer;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
