//Main Author: Marcus Mellström
//Secondary Author: Simon Sundström

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Highlight))]
public class PickUpObject : MonoBehaviour
{
    [SerializeField] private float impactDamage = 25f;
    [SerializeField] private int durability = 3;
    [SerializeField] private float holdingOpacity = 0.5f;

    public float ImpactDamage { get => impactDamage; set => impactDamage = value; }
    protected int Durability { get => durability; set => durability = value; }
    
    public Vector3 LastFramePosition { get; set; }
    
    protected bool IsThrown { get; set; }
    public bool IsColliding { get; private set; }

    protected Highlight Highlight { get; private set; }
    public Rigidbody RigidBody { get; private set; }
    protected MeshRenderer MeshRenderer { get; private set; }
    public Collider Collider { get; private set; }

    public Transform OriginalParent { get; set; }
    public Transform CurrentParent { get; set; }

    private float throwTimer = 0;
    private float thrownTime = 3.0f;

    private void Awake ()
    {
        OriginalParent = transform.parent;
        CurrentParent = OriginalParent;
        RigidBody = GetComponent<Rigidbody>();
        MeshRenderer = GetComponent<MeshRenderer>();
        Collider = GetComponent<Collider>();
        Highlight = GetComponent<Highlight>();
    }

    private void Update ()
    {
        if (IsThrown == true) {
            throwTimer += Time.deltaTime;
            if(throwTimer >= thrownTime) {
                IsThrown = false;
            }
        }
    }

    public void Hold (Vector3 pullPointPosition, Transform newParent)
    {
        gameObject.layer = LayerMask.NameToLayer("HoldingObject");
        CurrentParent = newParent;
        transform.SetParent(newParent);
        RigidBody.velocity = Vector3.zero;
        MeshRenderer.material.color = new Color(MeshRenderer.material.color.r, MeshRenderer.material.color.g, MeshRenderer.material.color.b, holdingOpacity);
    }

    public void Drop (bool isThrown, float throwForce)
    {
        gameObject.layer = LayerMask.NameToLayer("Pick Up Objects");
        RigidBody.useGravity = true;
        RigidBody.velocity = Vector3.zero;
        MeshRenderer.material.color = new Color(MeshRenderer.material.color.r, MeshRenderer.material.color.g, MeshRenderer.material.color.b, 1);
        CurrentParent = OriginalParent;
        transform.SetParent(OriginalParent);
        IsThrown = isThrown;
        if (isThrown) {
            RigidBody.AddForce(Camera.main.transform.forward * throwForce);
        }
    }

    public void Pull ()
    {
        IsThrown = false;
        LastFramePosition = transform.position;
        Highlight.Deactivate();
        RigidBody.useGravity = false;
    }

    protected void LoseDurability ()
    {
        Durability--;
        if (Durability <= 0) {
            ObjectDestroyedEvent objectDestroyedEvent = new ObjectDestroyedEvent(gameObject);
            objectDestroyedEvent.ExecuteEvent();
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (IsThrown) {
            if (other.CompareTag("Damageable")) {
                Debug.Log("Damage:" + ImpactDamage);
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

        if (collision.collider.CompareTag("DestructibleObject") && IsThrown) {
            collision.collider.GetComponent<DestructibleObject>().HitPoints -= ImpactDamage;
        }
    }

    private void OnCollisionExit (Collision collision)
    {
        IsColliding = false;
    }
}
