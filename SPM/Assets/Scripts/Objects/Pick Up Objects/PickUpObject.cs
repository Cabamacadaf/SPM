//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private Transform originalParent;
    protected Rigidbody rigidBody;
    protected MeshRenderer meshRenderer;

    [SerializeField] protected int durability = 3;
    [SerializeField] protected float impactDamage = 25f;
    [SerializeField] protected float lowestVelocityToDoDamage = 1.0f;
    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] private float holdingOpacity = 0.5f;
    private float pullForce;

    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;


    protected bool thrown = false;
    [HideInInspector] public bool isHighlighted = false;
    [HideInInspector] public bool holding = false;
    protected bool active = false;


    void Awake ()
    {
        originalParent = transform.parent;
        Debug.Log(originalParent);
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Holding (Vector3 pullPointPosition, Transform newParent)
    {
        transform.SetParent(newParent);
        transform.position = pullPointPosition;
        rigidBody.velocity = Vector3.zero;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, holdingOpacity);
    }

    public void Drop ()
    {
        Debug.Log(originalParent);
        rigidBody.useGravity = true;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1);
        transform.SetParent(originalParent);
        thrown = true;
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
        if (!isHighlighted) {
            isHighlighted = true;
            Color color = meshRenderer.material.color;
            meshRenderer.material = highlightedMaterial;
            meshRenderer.material.color = color;
        }
    }

    public void UnHighlight ()
    {
        if (isHighlighted) {
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
                enemyState.Damage(impactDamage);
                LoseDurability();
            }

            if (other.CompareTag("Enemy2Hurtbox")) {
                Enemy2 enemy = other.GetComponentInParent<Enemy2>();
                EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();
                enemyState.Damage(impactDamage * enemy.damageReduction);
                LoseDurability();
            }
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        //Debug.Log(rigidBody.velocity.magnitude);
        if (collision.collider.CompareTag("DestructibleObject") && rigidBody.velocity.magnitude >= lowestVelocityToDoDamage) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= impactDamage;
        }
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Geometry")) {
            //Debug.Log("Collided with Geometry");
        }
    }
}
