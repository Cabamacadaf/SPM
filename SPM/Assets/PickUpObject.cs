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

    void Awake()
    {
        player = FindObjectOfType<PlayerController3D>().transform;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        if(active && !holding) {
            transform.position += (pullPoint.position - transform.position).normalized * pullForce * Time.deltaTime;
            if (Vector3.Distance(transform.position, pullPoint.position) < 0.1f){
                rb.useGravity = false;
                holding = true;
            }
        }
    }

    public void Drop() {
        active = false;
        holding = false;
        rb.useGravity = true;
    }

    public void Pull (float pullForce, Transform pullPoint)
    {
        this.pullPoint = pullPoint;
        this.pullForce = pullForce;
        active = true;
    }
}
