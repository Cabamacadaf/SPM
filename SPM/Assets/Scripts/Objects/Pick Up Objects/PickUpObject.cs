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
    public new Collider collider { get; private set; }
    [SerializeField] private LayerMask collisionMask;

    [SerializeField] protected int durability = 3;
    [SerializeField] private float impactDamage = 25f;
    [SerializeField] protected float lowestVelocityToDoDamage = 1.0f;
    [SerializeField] private float holdingOpacity = 0.5f;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;

    protected bool isThrown = false;
    private bool isHighlighted = false;
    private bool isHeld = false;
    public bool IsColliding { get; private set; }

    private float throwTimer = 0;
    private float thrownTime = 1.0f;

    private void Awake ()
    {
        OriginalParent = transform.parent;
        CurrentParent = OriginalParent;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
    }

    private void Update ()
    {
        if (isThrown == true) {
            throwTimer += Time.deltaTime;
            if(throwTimer >= thrownTime) {
                isThrown = false;
            }
        }
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
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1);
        CurrentParent = OriginalParent;
        transform.SetParent(OriginalParent);
        isThrown = true;
    }

    public void Pull ()
    {
        rigidBody.useGravity = false;
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
        if (rigidBody.velocity.magnitude >= lowestVelocityToDoDamage && isThrown) {
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
        IsColliding = true;

        if (collision.collider.CompareTag("DestructibleObject") && rigidBody.velocity.magnitude >= lowestVelocityToDoDamage && isThrown) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= ImpactDamage;
        }
    }

    private void OnCollisionExit (Collision collision)
    {
        IsColliding = false;
    }
}
