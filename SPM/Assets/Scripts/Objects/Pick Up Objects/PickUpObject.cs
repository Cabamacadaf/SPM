//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Highlight))]
public class PickUpObject : MonoBehaviour
{
    public Transform OriginalParent { get; set; }
    public Transform CurrentParent { get; set; }

    public float ImpactDamage { get => impactDamage; set => impactDamage = value; }
    public Vector3 LastFramePosition { get; set; }

    protected Rigidbody rigidBody;
    protected MeshRenderer meshRenderer;
    protected Highlight highlight;
    public new Collider collider { get; private set; }

    [SerializeField] protected int durability = 3;
    [SerializeField] private float impactDamage = 25f;
    [SerializeField] private float holdingOpacity = 0.5f;
    
    protected bool isThrown = false;
    private bool isHeld = false;
    public bool IsColliding { get; private set; }

    private float throwTimer = 0;
    private float thrownTime = 3.0f;

    private void Awake ()
    {
        OriginalParent = transform.parent;
        CurrentParent = OriginalParent;
        rigidBody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        highlight = GetComponent<Highlight>();
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
        gameObject.layer = LayerMask.NameToLayer("HoldingObject");
        CurrentParent = newParent;
        transform.SetParent(newParent);
        rigidBody.velocity = Vector3.zero;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, holdingOpacity);
    }

    public void Drop (bool isThrown)
    {
        isHeld = false;
        gameObject.layer = LayerMask.NameToLayer("Pick Up Objects");
        rigidBody.useGravity = true;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1);
        CurrentParent = OriginalParent;
        transform.SetParent(OriginalParent);
        this.isThrown = isThrown;
    }

    public void Pull ()
    {
        LastFramePosition = transform.position;
        highlight.Deactivate();
        rigidBody.useGravity = false;
    }

    protected void LoseDurability ()
    {
        durability--;
        if (durability <= 0) {
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (isThrown) {
            if (other.CompareTag("Damageable")) {
                EnemyBaseState enemyState = (EnemyBaseState)other.GetComponentInParent<Enemy>().GetCurrentState();
                enemyState.Damage(ImpactDamage);
                LoseDurability();
            }

            if (other.CompareTag("Enemy2Hurtbox")) {
                Enemy2 enemy = other.GetComponentInParent<Enemy2>();
                EnemyBaseState enemyState = (EnemyBaseState)enemy.GetCurrentState();
                enemyState.Damage(ImpactDamage * enemy.DamageReduction);
                LoseDurability();
            }
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        IsColliding = true;

        if (collision.collider.CompareTag("DestructibleObject") && isThrown) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= ImpactDamage;
        }
    }

    private void OnCollisionExit (Collision collision)
    {
        IsColliding = false;
    }
}
