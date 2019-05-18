//Author: Marcus Mellström

using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : StateMachine
{
    #region Private Fields
    [SerializeField] private float hitPoints = 50.0f;
    private float maxHitPoints;

    [SerializeField] private float attackDamage = 25.0f;
    [SerializeField] private float attackDistance = 1.0f;
    [SerializeField] private float attackTime = 0.1f;
    [SerializeField] private float attackCooldown = 1.0f;
    [SerializeField] private float attackAnimationSpeed = 5.0f;

    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float rotationSpeed = 5.0f;

    [SerializeField] private float blastRecoveryTime = 2.0f;
 
    [SerializeField] private GameObject attackObject;

    [SerializeField] private LayerMask wallLayer;

    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip aggroSound;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip spawnSound;

    [SerializeField] private float idleSoundMinTime;
    [SerializeField] private float idleSoundMaxTime;
    #endregion

    #region Properties
    public float HitPoints { get => hitPoints; set => hitPoints = value; }
    public float MaxHitPoints { get; set; }

    public float AttackDamage { get => attackDamage; private set => attackDamage = value; }
    public float AttackDistance { get => attackDistance; private set => attackDistance = value; }
    public float AttackTime { get => attackTime; private set => attackTime = value; }
    public float AttackCooldown { get => attackCooldown; private set => attackCooldown = value; }
    public float AttackAnimationSpeed { get => attackAnimationSpeed; private set => attackAnimationSpeed = value; }

    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }
    public float Acceleration { get => acceleration; private set => acceleration = value; }
    public float RotationSpeed { get => rotationSpeed; private set => rotationSpeed = value; }

    public float BlastRecoveryTime { get => blastRecoveryTime; private set => blastRecoveryTime = value; }

    public GameObject AttackObject { get => attackObject; private set => attackObject = value; }

    public LayerMask WallLayer { get => wallLayer; private set => wallLayer = value; }

    public AudioClip AttackSound { get => attackSound; private set => attackSound = value; }
    public AudioClip AggroSound { get => aggroSound; private set => aggroSound = value; }
    public AudioClip IdleSound { get => idleSound; private set => idleSound = value; }
    public AudioClip SpawnSound { get => spawnSound; private set => spawnSound = value; }

    public float IdleSoundMinTime { get => idleSoundMinTime; private set => idleSoundMinTime = value; }
    public float IdleSoundMaxTime { get => idleSoundMaxTime; private set => idleSoundMaxTime = value; }

    public SkinnedMeshRenderer MeshRenderer { get; private set; }
    public BoxCollider BoxCollider { get; private set; }
    public Rigidbody RigidBody { get; private set; }
    public AudioSource AudioSource { get; private set; }
    public Light LightSource { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public PlayerController Player { get; private set; }
    #endregion

    protected override void Awake ()
    {
		Animator = GetComponent<Animator>();
        LightSource = GetComponentInChildren<Light>();
        AudioSource = GetComponent<AudioSource>();
        RigidBody = GetComponent<Rigidbody>();
        BoxCollider = GetComponent<BoxCollider>();
        Agent = GetComponent<NavMeshAgent>();
        MeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        Player = FindObjectOfType<PlayerController>();
        MaxHitPoints = hitPoints;
        base.Awake();
    }
}
