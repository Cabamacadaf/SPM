//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.AI;

public class Enemy : StateMachine
{
	[HideInInspector] public Animator animator; 

    public AudioClip attackSound;
    public AudioClip aggroSound;
    public AudioClip idleSound;
    public AudioClip spawnSound;
    public float idleSoundMinTime = 1.0f;
    public float idleSoundMaxTime = 5.0f;

    public float attackDistance = 15.0f;
    public float attackTime = 0.1f;
    public float attackCooldown = 1.0f;
    public float attackAnimationSpeed = 50.0f;
    public float attackDamage = 25f;

    public float hitPoints = 100f;
    public float movementSpeed = 5.0f;
    public float acceleration = 10.0f;
    public float rotationSpeed = 5.0f;
    public float blastRecoveryTime = 2.0f;
    [HideInInspector] public MeshRenderer meshRenderer;
    [HideInInspector] public NavMeshAgent agent;
    [HideInInspector] public BoxCollider boxCollider;
    public GameObject attackObject;
    [HideInInspector] public Rigidbody rigidBody;
    [HideInInspector] public AudioSource audioSource;
    [HideInInspector] public Light lightSource;
    public LayerMask wallLayer;
    [HideInInspector] public PlayerController player;

    protected override void Awake ()
    {
		animator = GetComponent<Animator>();
        lightSource = GetComponentInChildren<Light>();
        audioSource = GetComponent<AudioSource>();
        rigidBody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        agent = GetComponent<NavMeshAgent>();
        meshRenderer = GetComponent<MeshRenderer>();
        player = FindObjectOfType<PlayerController>();
        base.Awake();
    }
}
