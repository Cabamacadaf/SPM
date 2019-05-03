using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
    public AudioClip attackSound;
    public AudioClip aggroSound;
    public float attackDamage = 25f;
    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 5.0f;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public BoxCollider boxCollider;
    [HideInInspector] public GameObject attackHitbox;
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Light lightSource;

    [HideInInspector] public Player player;

    protected override void Awake ()
    {
        lightSource = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
        attackHitbox = transform.GetChild(0).gameObject;
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<Player>();
        base.Awake();
    }
}
