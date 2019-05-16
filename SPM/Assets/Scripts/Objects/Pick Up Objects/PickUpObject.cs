//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public Transform OriginalParent { get; set; }
    public Transform CurrentParent { get; set; }
    public float ImpactDamage { get => impactDamage; set => impactDamage = value; }


    protected Rigidbody rigidBody;
    protected MeshRenderer meshRenderer;
    private new Collider collider;
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] protected int durability = 3;
    [SerializeField] private float impactDamage = 25f;
    [SerializeField] protected float lowestVelocityToDoDamage = 1.0f;
    [SerializeField] private float holdingOpacity = 0.5f;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;

    private Vector3 lastFramePosition;
    private Vector3 velocity;
    public Vector3 NormalForce { get; private set; }

    protected bool isThrown = false;
    private bool isHighlighted = false;
    private bool isHeld = false;
    public bool IsColliding { get; private set; }

    private void Awake ()
    {
        lastFramePosition = transform.position;
        OriginalParent = transform.parent;
        CurrentParent = OriginalParent;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    private void Update ()
    {
        velocity = (transform.position - lastFramePosition) / Time.deltaTime;
        //Debug.Log("Velocity: " + velocity);
        lastFramePosition = transform.position;
        CheckCollision(0);
    }

    private void CheckCollision (float count)
    {
        if(count > 20) {
            return;
        }
        //Physics.BoxCast(collider.bounds.center, transform.localScale, velocity.normalized, out RaycastHit hit, transform.rotation, velocity.magnitude * Time.deltaTime, collisionMask);
        //if(hit.collider != null) {
        //    NormalForce = Functions.CalculateNormalForce(velocity, hit.normal);
        //    CheckCollision(count+1);
        //}
    }

    public void Hold (Vector3 pullPointPosition, Transform newParent)
    {
        isHeld = true;
        CurrentParent = newParent;
        transform.SetParent(newParent);
        rigidBody.velocity = Vector3.zero;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, holdingOpacity);
    }

    public void Drop ()
    {
        isHeld = false;
        rigidBody.useGravity = true;
        //rigidBody.constraints = RigidbodyConstraints.None;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1);
        CurrentParent = OriginalParent;
        transform.SetParent(OriginalParent);
        isThrown = true;
    }

    public void Pull ()
    {
        rigidBody.useGravity = false;
        //rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        UnHighlight();
    }

    protected void LoseDurability ()
    {
        durability--;
        if (durability <= 0) {
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }

    public void Highlight ()
    {
        if (isHighlighted == false) {
            isHighlighted = true;
            Color color = meshRenderer.material.color;
            meshRenderer.material = highlightedMaterial;
            meshRenderer.material.color = color;
        }
    }

    public void UnHighlight ()
    {
        if (isHighlighted == true) {
            isHighlighted = false;
            Color color = meshRenderer.material.color;
            meshRenderer.material = regularMaterial;
            meshRenderer.material.color = color;
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        Debug.Log("Velocity: " + rigidBody.velocity.magnitude);
        if (rigidBody.velocity.magnitude >= lowestVelocityToDoDamage) {
            if (other.CompareTag("Damageable")) {
                EnemyBaseState enemyState = (EnemyBaseState)other.GetComponentInParent<Enemy>().GetCurrentState();
                enemyState.Damage(ImpactDamage);
                LoseDurability();
            }

            if (other.CompareTag("Enemy2Hurtbox")) {
                Enemy2 enemy = other.GetComponentInParent<Enemy2>();
                EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();
                enemyState.Damage(ImpactDamage * enemy.damageReduction);
                LoseDurability();
            }
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        //IsColliding = true;

        //if(isHeld == true) {
        //    transform.SetParent(originalParent);
        //}

        if (collision.collider.CompareTag("DestructibleObject") && rigidBody.velocity.magnitude >= lowestVelocityToDoDamage) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= ImpactDamage;
        }
    }

    //private void OnCollisionExit (Collision collision)
    //{
    //    IsColliding = false;

    //    if(isHeld == true) {
    //        transform.SetParent(currentParent);
    //    }
    //}
}
