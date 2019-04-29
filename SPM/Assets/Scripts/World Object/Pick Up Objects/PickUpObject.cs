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
    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] protected float damageMultiplier = 0.2f;
    [SerializeField] protected float minDamage = 10f;
    [SerializeField] protected float maxDamage = 50f;
    [SerializeField] private float lowestVelocityToDoDamage = 5.0f;
    protected Transform pullPoint;
    protected bool thrown = false;

    //Should probably fix this
    private int geometry = 9;

    void Awake()
    {
        pullPoint = GameObject.Find("PullPoint").transform;
        player = FindObjectOfType<Player>().transform;
        rb = GetComponent<Rigidbody>();
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

                holding = true;
            }
        }
    }

    public void Drop()
    {
        //rb.useGravity = true;
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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Damageable") && rb.velocity.magnitude >= lowestVelocityToDoDamage) {
            EnemyBaseState enemyState = (EnemyBaseState)other.GetComponentInParent<Enemy>().GetCurrentState();
            enemyState.Damage(Mathf.Clamp(rb.velocity.magnitude * damageMultiplier, minDamage, maxDamage));
            LoseDurability();
        }
    }

    private void OnCollisionEnter (Collision collision)
    {
        if (collision.collider.CompareTag("DestructibleObject") && rb.velocity.magnitude >= lowestVelocityToDoDamage) {
            collision.collider.GetComponent<DestructibleObject>().hitPoints -= Mathf.Clamp(rb.velocity.magnitude * damageMultiplier, minDamage, maxDamage);
        }
    }
}
