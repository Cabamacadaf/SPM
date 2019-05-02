using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    [SerializeField] protected int durability;
    protected bool active = false;
    [HideInInspector] public bool holding = false;
    private Transform player;
    private float pullForce;
    protected Rigidbody rb;
    protected MeshRenderer meshRenderer;
    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] protected float impactDamage = 25f;
    [SerializeField] protected float lowestVelocityToDoDamage = 5.0f;
    [SerializeField] private float holdingOpacity = 0.5f;
    [SerializeField] private Material regularMaterial;
    [SerializeField] private Material highlightedMaterial;
    protected Transform pullPoint;
    protected bool thrown = false;
    [HideInInspector] public bool isHighlighted = false;

    //Should probably fix this
    private int geometry = 9;

    void Awake()
    {
        pullPoint = GameObject.Find("PullPoint").transform;
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }
    
    void Update()
    {
        if(active && !holding) {
            //rb.useGravity = false;
            //if (!(Vector3.Distance(transform.position, pullPoint.position) < distanceToGrab))
            //{
            //    transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;
            //}
            transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;

            if (Vector3.Distance(transform.position, pullPoint.position) < distanceToGrab)
            {
                transform.position = pullPoint.position;
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                //rb.useGravity = false;
                transform.SetParent(player.GetComponentInChildren<GravityGun>().transform);
                meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, holdingOpacity);

                holding = true;
            }
        }
    }

    public void Drop()
    {
        //rb.useGravity = true;
        meshRenderer.material.color = new Color(meshRenderer.material.color.r, meshRenderer.material.color.g, meshRenderer.material.color.b, 1);
        rb.isKinematic = false;
        transform.SetParent(null);
        active = false;
        holding = false;
        thrown = true;
    }

    public void Pull (float pullForce)
    {
        this.pullForce = pullForce;
        active = true;
    }

    protected void LoseDurability()
    {
        durability--;
        if(durability <= 0)
        {
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
        if (other.CompareTag("Damageable") && rb.velocity.magnitude >= lowestVelocityToDoDamage) {
            EnemyBaseState enemyState = (EnemyBaseState)other.GetComponentInParent<Enemy>().GetCurrentState();
            enemyState.Damage(impactDamage);
            LoseDurability();
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.CompareTag("DestructibleObject") && rb.velocity.magnitude >= lowestVelocityToDoDamage) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= impactDamage;
        }
    }
}
