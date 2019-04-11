using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private bool active = false;
    [HideInInspector] public bool holding = false;
    private Transform player;
    private float pullForce;
    private Transform pullPoint;
    private Rigidbody rb;

    [SerializeField] private float distanceToGrab = 0.1f;
    [SerializeField] private float damage = 10f;

    private int geometry = 9;

    void Awake()
    {
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
        rb.useGravity = true;
        rb.isKinematic = false;
        transform.SetParent(null);
        active = false;
        holding = false;
        //rb.useGravity = true;
    }

    public void Pull (float pullForce, Transform pullPoint)
    {
        this.pullPoint = pullPoint;
        this.pullForce = pullForce;
        active = true;
    }

    private void OnCollisionEnter (Collision collision)
    {

        if (collision.collider.CompareTag("Enemy")) {
            collision.collider.GetComponent<Enemy>().Damage(rb.velocity.magnitude, damage);
        }


    }
}
